using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public abstract class Container : Object
    {
        protected readonly List<Object> contents = new List<Object>();

        protected Container()
        {
            Describe = () =>
               {
                   if (IsEmpty)
                       return String.Format("The {0} is empty.", Name);
                   
                   if (contents.Count == 1)
                   {
                       Object child = contents[0];
                       return String.Format("In the {0} is {1} {2}.", Name, child.Article, child.Name);
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
                    result = "which is closed";
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
            Object obj = Net.Objects.Get<T>();
            obj.Parent = this;
            contents.Add(obj);
        }

        public void Add(Object obj) 
        {
            obj.Parent = this;
            contents.Add(obj);
        }

        public void Remove<T>() where T : Object
        {
            Object obj = Objects.Get<T>();
            Remove(obj);
        }

        public void Remove(Object obj) 
        {
            obj.Parent = null;
            contents.Remove(obj);
        }

        // is there a read only list???
        public IList<Object> Contents
        {
            get
            {
                var result = new List<Object>();
                result.AddRange(contents);
                return result;
            }
        }

        public bool Contains<T>() where T:Object
        {
            Object obj = Objects.Get<T>();
            return Contents.Contains(obj);
        }
          
    }
}
