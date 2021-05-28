using System;
using System.Linq;

namespace Adventure.Net
{
    public abstract class Verb 
    {
        public string Name { get; protected set; }
        
        public Synonyms Synonyms = new Synonyms();
        
        /// <summary>
        /// Summary accepts multiple items (objects in scope)
        /// </summary>
        public bool Multi { get; set; }

        /// <summary>
        /// Summary accepts multiple items (objects in inventory)
        /// </summary>
        public bool MultiHeld { get; set; }

        public static T Get<T>() where T : Verb
        {
            return (T)VerbList.List.Single(x => x is T);
        }

        public static Verb Get(Type type) 
        {
            return VerbList.List.Single(x => x.GetType() == type);
        }

        public static bool Redirect<T>(Item item, Func<T, bool> callback) where T : Verb
        {
            return item.Redirect(item, callback);
        }

        protected void Print(string message, CommandState? state = null)
        {
            Context.Current.Print(message, state);
        }
    }
}