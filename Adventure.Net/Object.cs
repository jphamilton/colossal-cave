using Adventure.Net.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;

public abstract class Object
{
    private readonly static List<char> vowels = new() { 'a', 'e', 'i', 'o', 'u' };
    private static string Theyre = "they're";
    private static string Thats = "that's";
    private static string Those = "those";
    private static string That = "that";
    private static string Are = "are";
    private static string Is = "is";

    private readonly Dictionary<Type, Func<bool>> beforeRoutines = new();
    private readonly AfterRoutines afterRoutines = new();
    private readonly Dictionary<Object, Func<Object, bool>> receiveRoutines = new();

    private bool isAbsent;
    private string definiteArticle;
    private string indefiniteArticle;

    public Action<bool> AbsentToggled { get; set; }

    private Dictionary<string, bool> attributes { get; } = new();

    public abstract void Initialize();

    protected Object()
    {
        Synonyms = new Synonyms();
    }

    public override string ToString()
    {
        return $"{Name}";
    }

    public string Name { get; set; }

    public Synonyms Synonyms { get; set; }


    public Action Daemon { get; set; }
    public bool DaemonStarted { get; set; }
    public string Description { get; set; }
    public string InitialDescription { get; set; }

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

    public string DefiniteArticle
    {
        get
        {
            if (definiteArticle == null)
            {
                definiteArticle = "the";
            }

            return definiteArticle;
        }

        set => definiteArticle = value;
    }

    public Func<string> Describe { get; set; }

    public string TheyreOrThats => PluralName ? Theyre : Thats;
    public string ThatOrThose => PluralName ? Those : That;
    public string IsOrAre => PluralName ? Are : Is;


    // attributes

    public bool Absent
    {
        get
        {
            return isAbsent;
        }

        set
        {
            isAbsent = value;
            AbsentToggled?.Invoke(isAbsent);
        }
    }

    public bool Animate { get; set; }
    public bool Edible { get; set; }
    public bool Light { get; set; }
    public bool Lockable { get; private set; }
    public bool Locked { get; set; }
    public bool On { get; set; }
    public bool Open { get; set; }
    public bool Openable { get; set; }
    public bool PluralName { get; set; }
    public bool Scenery { get; set; }
    public bool Static { get; set; }
    public bool Switchable { get; set; }
    public bool Touched { get; set; }
    public bool Transparent { get; set; }

    public void Attribute(string flag)
    {
        if (!attributes.ContainsKey(flag))
        {
            attributes[flag] = true;
        }
    }

    public bool Has(string attribute)
    {
        return attributes.ContainsKey(attribute);
    }

    public void LocksWithKey<T>(bool isLocked) where T : Object
    {
        Lockable = true;
        Locked = isLocked;
        Key = Objects.Get<T>();
    }

    public Object Key { get; private set; }



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

    public Func<bool> Before(Type verbType)
    {
        return beforeRoutines.ContainsKey(verbType) ? beforeRoutines[verbType] : null;
    }

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

    public void After<T>(Action after) where T : Verb
    {
        afterRoutines.Add(typeof(T), after);
    }

    public Action After<T>() where T : Verb
    {
        Type verbType = typeof(T);
        return After(verbType);
    }

    public Action After(Type verbType)
    {
        return afterRoutines.Get(verbType);
    }

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

    public void Receive(Func<Object, bool> beforeReceive)
    {
        receiveRoutines.Add(this, beforeReceive);
    }

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
    public static Object Noun
    {
        get { return Context.Current.Noun; }
    }

    // indirect object of current running command
    public static Object Second
    {
        get { return Context.Current.Second; }
    }

    public static T Get<T>() where T : Object
    {
        return Objects.Get<T>() ?? Rooms.Get<T>();
    }

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

    protected static bool In<T>() where T : Room
    {
        Object obj = Rooms.Get<T>();
        return (CurrentRoom.Location == obj);
    }

    protected static T Room<T>() where T : Room
    {
        return Rooms.Get(typeof(T)) as T;
    }

    public bool InScope
    {
        get
        {
            var scoped = CurrentRoom.ObjectsInScope();
            return scoped.Contains(this);
        }
    }

    public bool InInventory
    {
        get { return Inventory.Contains(this); }
    }

    public static bool IsCarrying<T>() where T : Object
    {
        return Inventory.Contains<T>();
    }

    public static void Remove<T>() where T : Object
    {
        var obj = Get<T>();
        obj.Remove();
    }

    public void Remove()
    {
        if (InInventory)
        {
            Inventory.Remove(this);
        }

        var containers = Inventory.Items.Where(obj => obj is Container);

        foreach (Container container in containers)
        {
            if (container.Contents.Contains(this))
            {
                container.Contents.Remove(this);
                break;
            }
        }

        ObjectMap.Remove(this);
    }

    public bool InRoom
    {
        get
        {

            return ObjectMap.Contains(CurrentRoom.Location, this);
        }
    }

    public void MoveToLocation()
    {
        Inventory.Remove(this);
        ObjectMap.MoveObject(this, CurrentRoom.Location);
    }

    public void MoveTo<T>() where T : Room
    {
        ObjectMap.MoveObject(this, Room<T>());
    }

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

    public Room Location
    {
        get
        {
            // obviously will not work well for scenery and
            // anything else that can be in multiple locations
            return ObjectMap.Location(this).FirstOrDefault();
        }
    }

    public bool IsHere<T>() where T : Object
    {
        return Get<T>().InRoom;
    }
}
