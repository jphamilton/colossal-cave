using Adventure.Net.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net
{

    public abstract class Item
    {
        private readonly Dictionary<Type, Func<bool>> beforeRoutines = new();
        private readonly AfterRoutines afterRoutines = new();
        private readonly Dictionary<Item, Func<Item, bool>> receiveRoutines = new();
        
        private bool isAbsent;
        public delegate void AbsentToggedHandler(bool absent);
        public event AbsentToggedHandler AbsentToggled;

        private Dictionary<string, bool> attributes { get; } = new();

        public abstract void Initialize();

        protected Item()
        {
            Synonyms = new Synonyms();
            Article = "a";
        }

        public override string ToString()
        {
            // TODO: How is it decided when/where to use Article?
            return $"{Name}";
        }

        public string Name { get; set; }
        public Synonyms Synonyms { get; set; }


        public Action Daemon { get; set; }
        public bool DaemonStarted { get; set; }
        public string Description { get; set; }
        public string InitialDescription { get; set; }

        public string Article { get; set; }
        public string TheyreOrThats => HasPluralName ? "They're" : "That's";
        public string ThatOrThose => HasPluralName ? "Those" : "That";
        public string IsOrAre => HasPluralName ? "are" : "is";


        // attributes
        public bool HasLight { get; set; }
        public bool HasPluralName { get; set; }

        public bool IsAbsent
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
        public bool Lockable { get; private set; }
        public bool Locked { get; set; }
        public bool On { get; set; }    // on or off?
        public bool Open { get; set; }
        public bool Openable { get; set; }
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

        // Locking/Unlocking

        public void LocksWithKey<T>(bool isLocked) where T : Item
        {
            Lockable = true;
            Locked = isLocked;
            Key = Objects.Get<T>();
        }

        public Item Key { get; private set; }

        //

        public Func<string> Describe { get; set; }

        public void Before<T>(Func<string> before) where T: Verb
        {
            bool wrapper()
            {
                var message = before();

                if (message != null)
                {
                    return Print(before());
                }

                return false;
            }

            Before<T>(wrapper);
        }

        public void Before<T>(Func<bool> before) where T : Verb
        {
            if (typeof(T) == typeof(Receive))
            {
                throw new NotSupportedException("Before<Receive> is not supported. Use Receive instead.");
            }

            // TODO: needed this for testing. do this a different way
            if (beforeRoutines.ContainsKey(typeof(T)))
            {
                beforeRoutines.Remove(typeof(T));
            }

            beforeRoutines.Add(typeof(T), before);
        }

        public Func<bool> Before<T>() where T : Verb
        {
            var verbType = typeof(T);
            return Before(verbType);
        }

        public Func<bool> Before(Type verbType)
        {
            if (beforeRoutines.ContainsKey(verbType))
                return beforeRoutines[verbType];
            return null;
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

        public void Receive(Func<Item, string> beforeReceive)
        {
            bool wrapper(Item obj)
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

        public void Receive(Func<Item, bool> beforeReceive)
        {
            receiveRoutines.Add(this, beforeReceive);
        }

        public Func<Item, bool> Receive()
        {
            if (receiveRoutines.TryGetValue(this, out Func<Item, bool> result))
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
        public static Item CurrentObject
        {
            get { return Context.Current.CurrentObject; }
        }

        // indirect object of current running command
        public static Item IndirectObject
        {
            get { return Context.Current.IndirectObject; }
        }

        public static T Get<T>() where T : Item
        {
            return Objects.Get<T>();
        }

        public static bool Redirect<T>(Item obj, Func<T, bool> callback) where T : Verb
        {
            var command = Context.Current.PushState();

            var handled = RunCommand(command, obj, callback);

            Context.Current.PopState();

            return handled;
        }

        internal static ExecuteResult Execute<T>(Item obj, Func<T, bool> callback) where T : Verb
        {
            var commandOutput = new CommandOutput();
            var command = Context.Current.PushState(commandOutput);

            var handled = RunCommand(command, obj, callback);

            Context.Current.PopState(commandOutput);

            return new ExecuteResult(handled, commandOutput);
        }

        private static bool RunCommand<T>(ICommandState command, Item obj, Func<T, bool> callback) where T : Verb
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
            Item obj = Rooms.Get<T>();
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

        public static bool IsCarrying<T>() where T : Item
        {
            return Inventory.Contains<T>();
        }

        public static void Remove<T>() where T : Item
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
    }
}
