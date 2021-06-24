using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public abstract class Container : Object
    {
        protected List<Object> contents = new();
        
        protected Container()
        {
            Describe = () =>
               {
                   if (IsEmpty)
                       return $"The {Name} is empty.";
                   
                   if (contents.Count == 1)
                   {
                       Object child = contents[0];
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
                
                if (!Open)
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

        public void Add<T>() where T:Object
        {
            Object obj = Objects.Get<T>();
            contents.Add(obj);
        }

        public void Add(Object obj) 
        {
            contents.Add(obj);
        }

        public void Remove(Object obj) 
        {
            contents.Remove(obj);
        }

        public new void Remove<T>() where T : Object
        {
            var obj = Objects.Get<T>();
            contents.Remove(obj);
        }

        public IList<Object> Contents
        {
            get
            {
                return contents;
            }
        }

        public bool Contains<T>() where T:Object
        {
            Object obj = Objects.Get<T>();
            return Contents.Contains(obj);
        }
          
    }
}
