using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public abstract class Object
    {
        
        protected static Library L = new Library();

        private readonly Dictionary<Type, Func<bool>> beforeRoutines;
        private readonly Dictionary<Type, Func<bool>> afterRoutines;

        protected Object()
        {
            beforeRoutines = new Dictionary<Type, Func<bool>>();
            afterRoutines = new Dictionary<Type, Func<bool>>();
            Synonyms = new Synonyms();
            Article = "a";
        }
        
        public string Article { get; set; }

        public Action Daemon { get; set; }
        public bool DaemonStarted { get; set; }

        public string Description { get; set; }
        
        public string InitialDescription { get; set; }
        
        public bool HasActionRountines
        {
            get { return beforeRoutines.Count > 0 || afterRoutines.Count > 0; }
        }

        
        public bool HasLight { get; set; }
        public bool HasPluralName { get; set; }

        public bool IsAnimate { get; set; }
        public bool IsEdible { get; set; }
        public bool IsLockable { get; set; }
        public bool IsLocked { get; set; }
        public bool IsOn { get; set; }    // on or off?
        public bool IsOpen { get; set; }
        public bool IsOpenable { get; set; }
        public bool IsScenery { get; set; }
        public bool IsStatic { get; set; }
        public bool IsSwitchable { get; set; }
        public bool IsTouched { get; set; }
        public bool IsTransparent { get; set; }

        
        public string Name { get; set; }
        public Object Parent { get; set; }
        public Synonyms Synonyms { get; set; }
        
        public abstract void Initialize();

        public Func<string> Describe { get; set; }

        public void Before<T>(Func<bool> before) where T : Verb
        {
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

        public void After<T>(Func<bool> after) where T : Verb
        {
            afterRoutines.Add(typeof(T), after);
        }

        public Func<bool> After<T>() where T : Verb
        {
            Type verbType = typeof (T);
            return After(verbType);
        }

        public Func<bool> After(Type verbType)
        {
            if (afterRoutines.ContainsKey(verbType))
                return afterRoutines[verbType];
            return null;
        }

        public bool Is<T>()
        {
            return (GetType() == typeof(T));
        }

        public string TheyreOrThats
        {
            get
            {
                return HasPluralName ? "They're" : "That's";
            }
        }

        public string ThatOrThose
        {
            get
            {
                return HasPluralName ? "Those" : "That";
            }
        }

        public string IsOrAre
        {
            get
            {
                return HasPluralName ? "are" : "is";
            }
        }

        #region Convenience Methods for Action Routines

        protected void Print(string message)
        {
            Context.Parser.Print(message);
        }

        protected void Print(string format, params object[] arg)
        {
            Context.Parser.Print(format, arg);
        }

        protected void Execute(string input)
        {
            var userInput = new UserInput();
            var inputResult = userInput.Parse(input);
            var builder = new CommandBuilder(inputResult);
            var commands = builder.Build();

            foreach (var command in commands)
            {
                Context.Parser.ExecuteCommand(command);
            }
           
        }

        protected bool In<T>() where T:Object
        {
            Object obj = Rooms.Get<T>();
            return (L.CurrentLocation == obj);
        }

        protected Room Room<T>()
        {
            return Rooms.Get<T>();
        }
        
        protected Room Location
        {
            get { return Context.Story.Location; }
        }
        
        public bool InScope
        {
            get
            {
                var scoped = L.ObjectsInScope();
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
                Inventory.Remove(this);
            if (InScope)
                Context.Story.Location.Objects.Remove(this);
        }

        public bool AtLocation
        {
            get { return Context.Story.Location.Objects.Contains(this); }
        }

        public bool IsContainer
        {
            get { return (this as Container != null);  }
        }

        public void MoveToLocation()
        {
            Inventory.Remove(this);
            Context.Story.Location.Objects.Add(this);
        }

        #endregion

    }
}
