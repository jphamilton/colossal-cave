using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public abstract class Container : Item
    {
        protected List<Item> contents = new();
        
        protected Container()
        {
            Describe = () =>
               {
                   if (IsEmpty)
                       return $"The {Name} is empty.";
                   
                   if (contents.Count == 1)
                   {
                       Item child = contents[0];
                       return $"In the {Name} {IsOrAre} {child.Article} {child.Name}.";
                   }
                   
                   throw new Exception("Don't know how to deal with containers with more than one thing.");

               };
        }

        public string State
        {
            get
            {
                string result;
                
                if (!IsOpen)
                {
                    result = "which is closed";
                }
                else
                {
                    result = Contents.Count > 0 ? "which is open" : "which is open but empty";
                }

                return result;
            }
        }

        public bool IsEmpty
        {
            get { return contents.Count == 0; }
        }

        public void Empty()
        {
            contents.Clear();
        }

        public void Add<T>() where T:Item
        {
            Item obj = Objects.Get<T>();
            contents.Add(obj);
        }

        public void Add(Item obj) 
        {
            contents.Add(obj);
        }

        public void Remove(Item obj) 
        {
            contents.Remove(obj);
        }

        public new void Remove<T>() where T : Item
        {
            var obj = Objects.Get<T>();
            contents.Remove(obj);
        }

        public IList<Item> Contents
        {
            get
            {
                return contents;
            }
        }

        public bool Contains<T>() where T:Item
        {
            Item obj = Objects.Get<T>();
            return Contents.Contains(obj);
        }
          
    }
}
