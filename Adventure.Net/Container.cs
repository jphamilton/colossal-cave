using System;
using System.Collections.Generic;

namespace Adventure.Net;

public abstract class Container : Object
{
    protected Container()
    {
        Describe = () =>
           {
               if (IsEmpty)
               {
                   return $"The {Name} is empty.";
               }

               if (Children.Count == 1)
               {
                   Object child = Children[0];
                   return $"In the {Name} {IsOrAre} {child.DefiniteArticle} {child.Name}.";
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
                result = Children.Count > 0 ? "which is open" : "which is open but empty";
            }

            return result;
        }
    }

    public bool IsEmpty
    {
        get { return Children.Count == 0; }
    }

    public void Empty()
    {
        Children.Clear();
    }

    public void Add<T>() where T : Object
    {
        Object obj = Objects.Get<T>();
        Children.Add(obj);
    }

    public void Add(Object obj)
    {
        Children.Add(obj);
    }

    public void Remove(Object obj)
    {
        Children.Remove(obj);
    }

    public new void Remove<T>() where T : Object
    {
        var obj = Objects.Get<T>();
        Children.Remove(obj);
    }

    //public IList<Object> Contents
    //{
    //    get
    //    {
    //        return Children;
    //    }
    //}

    public bool Contains<T>() where T : Object
    {
        return Children.Contains(Objects.Get<T>());
    }

    public bool Contains(Object obj)
    {
        return Children.Contains(obj);
    }
}
