using Adventure.Net.Verbs;
using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public abstract class Item
    {
        private readonly Dictionary<Type, Func<bool>> beforeRoutines = new Dictionary<Type, Func<bool>>();
        private readonly Dictionary<Type, Action> afterRoutines = new Dictionary<Type, Action>();
        private readonly Dictionary<Item, Func<Item, bool>> receiveRoutines = new Dictionary<Item, Func<Item, bool>>();

        public abstract void Initialize();

        protected Item()
        {
            Synonyms = new Synonyms();
            Article = "a";
        }

        public string Name { get; set; }
        public Synonyms Synonyms { get; set; }
        public Item Parent { get; set; }


        public string Article { get; set; }

        public Action Daemon { get; set; }
        public bool DaemonStarted { get; set; }
        public string Description { get; set; }
        public string InitialDescription { get; set; }

        public string TheyreOrThats => HasPluralName ? "They're" : "That's";
        public string ThatOrThose => HasPluralName ? "Those" : "That";
        public string IsOrAre => HasPluralName ? "are" : "is";


        // attributes
        public bool HasLight { get; set; }
        public bool HasPluralName { get; set; }
        public bool IsAnimate { get; set; }
        public bool IsEdible { get; set; }
        public bool IsLockable { get; private set; }
        public bool IsLocked { get; set; }
        public bool IsOn { get; set; }    // on or off?
        public bool IsOpen { get; set; }
        public bool IsOpenable { get; set; }
        public bool IsScenery { get; set; }
        public bool IsStatic { get; set; }
        public bool IsSwitchable { get; set; }
        public bool IsTouched { get; set; }
        public bool IsTransparent { get; set; }

        // Locking/Unlocking
        
        public void LocksWithKey<T>(bool isLocked) where T : Item
        {
            IsLockable = true;
            IsLocked = isLocked;
            Key = Net.Objects.Get<T>();
        }

        public Item Key { get; private set; }
        
        //

        public Func<string> Describe { get; set; }

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
            var verbType = typeof (T);
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
            Type verbType = typeof (T);
            return After(verbType);
        }

        public Action After(Type verbType)
        {
            if (afterRoutines.ContainsKey(verbType))
                return afterRoutines[verbType];
            return null;
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

        protected void Print(string message)
        {
            Context.Current.Print(message);
        }

        // current object being handled by the command handler
        public Item CurrentObject
        {
            get { return Context.Current.CurrentObject; }
        }

        // indirect object of current running command
        public Item IndirectObject
        {
            get { return Context.Current.IndirectObject; }
        }

        public static T Get<T>() where T: Item
        {
            return Objects.Get<T>();
        }

        public bool Redirect<T>(Item obj, Func<T, bool> callback) where T : Verb
        {
            var command = Context.Current.PushState();

            var handled = RunCommand(command, obj, callback);

            Context.Current.PopState();

            return handled;
        }

        internal ExecuteResult Execute<T>(Item obj, Func<T, bool> callback) where T : Verb
        {
            var commandOutput = new CommandOutput();
            var command = Context.Current.PushState(commandOutput);

            var handled = RunCommand(command, obj, callback);

            Context.Current.PopState(commandOutput);

            return new ExecuteResult(handled, commandOutput);
        }

        private bool RunCommand<T>(ICommandState command, Item obj, Func<T, bool> callback) where T: Verb
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

        protected bool In<T>() where T:Item
        {
            Item obj = Rooms.Get<T>();
            return (CurrentRoom.Location == obj);
        }

        protected Room Room<T>()
        {
            return Rooms.Get<T>();
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

        public void Remove()
        {
            if (InInventory)
            {
                Inventory.Remove(this);
            }

            if (InScope)
            {
                Context.Story.Location.Objects.Remove(this);
            }
        }

        public bool InRoom
        {
            get { return Context.Story.Location.Objects.Contains(this); }
        }

        public void MoveToLocation()
        {
            Inventory.Remove(this);
            Context.Story.Location.Objects.Add(this);
        }

    }
}
