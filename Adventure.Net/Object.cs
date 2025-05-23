using Adventure.Net.ActionRoutines;
using Adventure.Net.Extensions;
using Adventure.Net.Things;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;

namespace Adventure.Net;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public abstract class Object
{
    private readonly static List<char> vowels = ['a', 'e', 'i', 'o', 'u'];
    private const string They = "they";
    private const string Theyre = "they're";
    private const string Thats = "that's";
    private const string Those = "those";
    private const string That = "that";
    private const string Are = "are";
    private const string Is = "is";
    private const string It = "it";

    private readonly Dictionary<Type, Func<bool>> beforeRoutines = [];
    private readonly AfterRoutines afterRoutines = new();
    private readonly Dictionary<Object, Func<Object, bool>> receiveRoutines = [];

    private string definiteArticle;
    private string indefiniteArticle;

    public abstract void Initialize();

    public int Id { get; set; } // used internally for serialization

    // "the brass lantern"
    public string DName => $"{DefiniteArticle} {Name}";

    // "a brass lantern"
    public string IName => $"{IndefiniteArticle} {Name}";

    [JsonIgnore]
    public Object Parent { get; set; }

    [JsonIgnore]
    public IList<Object> Children { get; set; } = [];

    [JsonIgnore]
    public Synonyms Synonyms { get; set; } = [];

    [JsonIgnore]
    public string Name { get; set; }

    [JsonIgnore]
    public bool PluralName { get; set; }

    /// <summary>
    /// This runs once per turn and provides clock-like behavior.
    /// Useful for moving things around like NPC's and draining
    /// batteries in a brass lantern
    /// </summary>
    [JsonIgnore]
    public Action Daemon { get; set; }

    /// <summary>
    /// Normal every day object description
    /// </summary>
    [JsonIgnore]
    public string Description { get; set; }

    /// <summary>
    /// Useful for changing an Object's description based on game conditions
    /// </summary>
    [JsonIgnore]
    public Func<string> Describe { get; set; }

    /// <summary>
    /// Description shown when player visits room for the first time
    /// </summary>
    [JsonIgnore]
    public string InitialDescription { get; set; }

    public string TheyreOrThats => PluralName ? Theyre : Thats;
    public string ThatOrThose => PluralName ? Those : That;
    public string IsOrAre => PluralName ? Are : Is;
    public string TheyOrIt => PluralName ? They : It;

    #region Object Attributes

    public bool Absent { get; set; }                    // When true, Object is currently unavailable/not visible at it's location
    public bool Animate { get; set; }                   // Is alive
    public bool Clothing { get; set; }                  // Is clothing that can be worn
    public bool Edible { get; set; }                    // Can be eaten
    public bool Light { get; set; }                     // Provides light
    public bool Lockable { get; set; }          // Can be locked
    public bool Locked { get; set; }                    // Is locked/unlocked
    public bool Multitude { get; set; }                 // more than one (e.g. a pile of leaves)
    public bool On { get; set; }                        // Is on/off
    public bool Open { get; set; }                      // Is open/closed
    public bool Openable { get; set; }                  // Can open/close
    public bool Scenery { get; set; }                   // Object is described in the text of the room and will not be listed with the other objects present.
    public bool Static { get; set; }                    // Object is fixed in place
    public bool Switchable { get; set; }                // Can turn on/off
    public bool Touched { get; set; }                   // Has been picked up
    public bool Transparent { get; set; }               // See-thru
    public bool Worn { get; set; }                      // Clothing is being worn
    public bool DaemonRunning { get; set; }             // Daemon is running

    public void LocksWithKey<T>(bool isLocked) where T : Object
    {
        Lockable = true;
        Locked = isLocked;
        Key = Objects.Get<T>();
    }

    /// <summary>
    /// The key that opens this object
    /// </summary>
    [JsonIgnore]
    public Object Key { get; set; }

    #endregion

    #region Before Action Helpers

    public Func<bool> GetBeforeRoutine(Type verbType) => beforeRoutines.TryGetValue(verbType, out var value) ? value : null;

    public bool Before<T>() where T : Routine
    {
        return beforeRoutines.Any(x => x.Key == typeof(T));
    }

    public void Before<T>(Func<string> before) where T : Routine
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
        where T : Routine
        where R : Routine
    {
        Before<T>(before);
        Before<R>(before);
    }

    public void Before<T, R, S>(Func<bool> before)
        where T : Routine
        where R : Routine
        where S : Routine
    {
        Before<T>(before);
        Before<R>(before);
        Before<S>(before);
    }

    public void Before<T>(Func<bool> before) where T : Routine
    {
        var type = typeof(T);

        var subTypes = Routines.List?.Where(x => x.GetType().IsSubclassOf(type)).ToList() ?? new List<Routine>();

        beforeRoutines.TryAdd(type, before);

        foreach (var subType in subTypes)
        {
            beforeRoutines.TryAdd(subType.GetType(), before);
        }
    }

    #endregion

    #region After Action Helpers

    public Action GetAfterRoutine(Type verbType) => afterRoutines.Get(verbType);

    public void After<T>(Func<string> after) where T : Routine
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

    public void After<T>(Action after) where T : Routine => afterRoutines.Add(typeof(T), after);

    #endregion

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
        }
        ;

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

    #region Print Helpers

    protected static bool Print(string message)
    {
        message = message.Capitalize();
        Output.Print(message);
        return true;
    }

    protected static bool Success(string message)
    {
        message = message.Capitalize();
        Output.Print(message);
        return true;
    }

    protected static bool Fail(string message)
    {
        message = message.Capitalize();
        Output.Print(message);
        return false;
    }

    #endregion

    /// <summary>
    /// Provides access to Object in Before/After routines
    /// </summary>
    public static Object First => Context.Current.First;

    /// <summary>
    /// Provides access to indirect Object in Before/After routines
    /// </summary>
    public static Object Second => Context.Current.Second;

    /// <summary>
    /// Get any Object
    /// </summary>
    public static T Get<T>() where T : Object => Objects.Get<T>();

    /// <summary>
    /// Gets a Room
    /// </summary>
    protected static T Room<T>() where T : Room => Rooms.Get<T>();

    /// <summary>
    /// Return true, if object is in scope, otherwise false
    /// </summary>
    public bool InScope => CurrentRoom.ObjectsInScope().Contains(this);

    /// <summary>
    /// Removes object from object tree
    /// </summary>
    public static void Remove<T>() where T : Object
    {
        var obj = Get<T>();
        obj.Remove();
    }

    /// <summary>
    /// Removes and Object from the object tree
    /// </summary>
    public void Remove() => ObjectMap.Remove(this);

    /// <summary>
    /// Returns true if the Object is in the current location, otherwise false
    /// </summary>
    public bool InRoom => ObjectMap.Contains(Player.Location, this);

    /// <summary>
    /// Moves Object to current location
    /// </summary>
    public void MoveToLocation() => ObjectMap.MoveObject(this, Player.Location);

    /// <summary>
    /// Move object to given location
    /// </summary>
    /// <typeparam name="T">Room</typeparam>
    public void MoveTo<T>() where T : Room => ObjectMap.MoveObject(this, Room<T>());

    /// <summary>
    /// Object current location
    /// </summary>
    public Room Location => ObjectMap.Location(this);

    #region Object Location Helpers

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

    #endregion

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

    private string DebuggerDisplay => Name != null ? $"{Name} in {Location}" : $"{GetType().Name}";

    // triggers "death"
    protected void Dead()
    {
        throw new DeathException();
    }
}
