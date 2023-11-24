using Adventure.Net.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Adventure.Net;


public abstract class Object
{
    private readonly static List<char> vowels = ['a', 'e', 'i', 'o', 'u'];
    private const string Theyre = "they're";
    private const string Thats = "that's";
    private const string Those = "those";
    private const string That = "that";
    private const string Are = "are";
    private const string Is = "is";

    private readonly Dictionary<Type, Func<bool>> beforeRoutines = [];
    private readonly AfterRoutines afterRoutines = new();
    private readonly Dictionary<Object, Func<Object, bool>> receiveRoutines = [];

    private string definiteArticle;
    private string indefiniteArticle;

    public abstract void Initialize();

    // used internally for serialization
    public int Id { get; set; }

    [JsonIgnore]
    public Object Parent { get; set; }

    [JsonIgnore]
    public IList<Object> Children { get; set; } = new List<Object>();

    [JsonIgnore]
    public Synonyms Synonyms { get; set; }

    protected Object()
    {
        Synonyms = new Synonyms();
    }

    public override string ToString() => $"{Name}";

    [JsonIgnore]
    public string Name { get; set; }
    
    [JsonIgnore]
    public bool PluralName { get; set; }
    
    [JsonIgnore]
    public Action Daemon { get; set; }
    
    [JsonIgnore]
    public string Description { get; set; }
    
    [JsonIgnore]
    public string InitialDescription { get; set; }

    [JsonIgnore]
    public string IndefiniteArticle
    {
        get
        {
            if (string.IsNullOrEmpty(indefiniteArticle) && !string.IsNullOrEmpty(Name))
            {
                var startsWithVowel = vowels.Contains(Name.ToLower().First());
                indefiniteArticle = startsWithVowel && !PluralName ? "an" : "a";
            }
            return indefiniteArticle;
        }
        set { indefiniteArticle = value; }
    }

    [JsonIgnore]
    public string DefiniteArticle
    {
        get
        {
            if (string.IsNullOrEmpty(definiteArticle))
            {
                definiteArticle = "the";
            }

            return definiteArticle;
        }

        set => definiteArticle = value;
    }

    [JsonIgnore]
    public Func<string> Describe { get; set; }

    public string TheyreOrThats => PluralName ? Theyre : Thats;
    public string ThatOrThose => PluralName ? Those : That;
    public string IsOrAre => PluralName ? Are : Is;

    // attributes
    public bool Absent { get; set; }
    public bool Animate { get; set; }
    public bool Clothing { get; set; }
    public bool Edible { get; set; }
    public bool Light { get; set; }
    public bool Lockable { get; private set; }
    public bool Locked { get; set; }
    public bool Multitude { get; set; }
    public bool On { get; set; }
    public bool Open { get; set; }
    public bool Openable { get; set; }
    // Object is a feature of its location
    public bool Scenery { get; set; }
    // Object is fixed in place
    public bool Static { get; set; }
    public bool Switchable { get; set; }
    public bool Touched { get; set; }
    public bool Transparent { get; set; }
    public bool Worn { get; set; }

    public bool DaemonStarted { get; set; }

    public void LocksWithKey<T>(bool isLocked) where T : Object
    {
        Lockable = true;
        Locked = isLocked;
        Key = Objects.Get<T>();
    }

    [JsonIgnore]
    public Object Key { get; set; }

    public void Before<T>(Func<string> before) where T : Verb
    {
        bool wrapper()
        {
            var message = before();

            if (message != null)
            {
                return Print(message);
            }

            return false;
        }

        Before<T>(wrapper);
    }

    public void Before<T, R>(Func<bool> before)
        where T : Verb
        where R : Verb
    {
        Before<T>(before);
        Before<R>(before);
    }

    public void Before<T, R, S>(Func<bool> before)
        where T : Verb
        where R : Verb
        where S : Verb
    {
        Before<T>(before);
        Before<R>(before);
        Before<S>(before);
    }

    public void Before<T>(Func<bool> before) where T : Verb
    {
        if (typeof(T) == typeof(Receive))
        {
            throw new NotSupportedException("Before<Receive> is not supported. Use Receive instead.");
        }

        beforeRoutines.Remove(typeof(T));
        beforeRoutines.Add(typeof(T), before);
    }

    public Func<bool> Before<T>() where T : Verb
    {
        var verbType = typeof(T);
        return Before(verbType);
    }

    public Func<bool> Before(Type verbType) => beforeRoutines.ContainsKey(verbType) ? beforeRoutines[verbType] : null;

    public void After<T>(Func<string> after) where T : Verb
    {
        void wrapper()
        {
            var message = after();

            if (message != null)
            {
                Print(message);
            }
        }

        After<T>(wrapper);
    }

    public void After<T>(Action after) where T : Verb => afterRoutines.Add(typeof(T), after);

    public Action After<T>() where T : Verb
    {
        Type verbType = typeof(T);
        return After(verbType);
    }

    public Action After(Type verbType) => afterRoutines.Get(verbType);

    public void Receive(Func<Object, string> beforeReceive)
    {
        bool wrapper(Object obj)
        {
            var message = beforeReceive(obj);

            if (message != null)
            {
                return Print(message);
            }

            return false;
        };

        Receive(wrapper);
    }

    public void Receive(Func<Object, bool> beforeReceive) => receiveRoutines.Add(this, beforeReceive);

    public Func<Object, bool> Receive()
    {
        if (receiveRoutines.TryGetValue(this, out Func<Object, bool> result))
        {
            return result;
        }

        return null;
    }

    public static bool Print(string message)
    {
        if (Context.Current != null)
        {
            Context.Current.Print(message);
        }
        else
        {
            Output.Print(message);
        }

        return true;
    }

    // current object being handled by the command handler
    public static Object Noun => Context.Current.Noun;

    // indirect object of current running command
    public static Object Second => Context.Current.Second;

    public static T Get<T>() where T : Object => Objects.Get<T>();

    public static bool Redirect<T>(Object obj, Func<T, bool> callback) where T : Verb
    {
        var command = Context.Current.PushState();

        var handled = RunCommand(command, obj, callback);

        Context.Current.PopState();

        return handled;
    }

    internal static ExecuteResult Execute<T>(Object obj, Func<T, bool> callback) where T : Verb
    {
        var commandOutput = new CommandOutput();
        var command = Context.Current.PushState(commandOutput);

        var handled = RunCommand(command, obj, callback);

        Context.Current.PopState(commandOutput);

        return new ExecuteResult(handled, commandOutput);
    }

    private static bool RunCommand<T>(ICommandState command, Object obj, Func<T, bool> callback) where T : Verb
    {
        var handled = false;
        var success = false;

        var before = obj.Before<T>();

        if (before != null)
        {
            command.State = CommandState.Before;
            handled = before();
        }

        if (!handled)
        {
            command.State = CommandState.During;
            success = callback(Verb.Get<T>());

            var after = obj.After<T>();

            if (success && after != null)
            {
                command.State = CommandState.After;
                after();
            }
        }

        return success;
    }

    public static bool In<T>() where T : Room
    {
        Object room = Rooms.Get<T>();
        return (CurrentRoom.Location == room);
    }

    protected static T Room<T>() where T : Room => Rooms.Get<T>();

    public bool InScope => CurrentRoom.ObjectsInScope().Contains(this);
    public static bool IsCarrying<T>() where T : Object => Inventory.Contains<T>();
    public static bool IsCarrying(Object obj) => Inventory.Contains(obj);

    public static void Remove<T>() where T : Object
    {
        var obj = Get<T>();
        obj.Remove();
    }

    public void Remove() => ObjectMap.Remove(this);

    public bool InRoom => ObjectMap.Contains(CurrentRoom.Location, this);

    public void MoveToLocation() => ObjectMap.MoveObject(this, CurrentRoom.Location);

    public void MoveTo<T>() where T : Room => ObjectMap.MoveObject(this, Room<T>());

    public Room Location => ObjectMap.Location(this);

    public bool IsHere<T>() where T : Object => Get<T>().InRoom;

    public void FoundIn<R>() where R : Room
    {
        var room = Room<R>();
        ObjectMap.Add(this, room);
    }

    public void FoundIn<R1, R2>() where R1 : Room where R2 : Room
    {
        FoundIn<R1>();
        FoundIn<R2>();
    }

    public void FoundIn<R1, R2, R3>() where R1 : Room where R2 : Room where R3 : Room
    {
        FoundIn<R1, R2>();
        FoundIn<R3>();
    }

    public void FoundIn<R1, R2, R3, R4>() where R1 : Room where R2 : Room where R3 : Room where R4 : Room
    {
        FoundIn<R1, R2, R3>();
        FoundIn<R4>();
    }

    public void FoundIn<R1, R2, R3, R4, R5>()
        where R1 : Room where R2 : Room where R3 : Room where R4 : Room where R5 : Room
    {
        FoundIn<R1, R2, R3, R4>();
        FoundIn<R5>();
    }

    public void FoundIn<R1, R2, R3, R4, R5, R6>()
        where R1 : Room where R2 : Room where R3 : Room where R4 : Room where R5 : Room
        where R6 : Room
    {
        FoundIn<R1, R2, R3, R4, R5>();
        FoundIn<R6>();
    }

    public void FoundIn<R1, R2, R3, R4, R5, R6, R7>()
        where R1 : Room where R2 : Room where R3 : Room where R4 : Room where R5 : Room
        where R6 : Room where R7 : Room
    {
        FoundIn<R1, R2, R3, R4, R5, R6>();
        FoundIn<R7>();
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            foreach (char c in GetType().FullName)
            {
                hash = (hash * 31) + c;
            }
            return hash;
        }
    }
}
