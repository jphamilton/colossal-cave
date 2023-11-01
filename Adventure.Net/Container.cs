using Adventure.Net.Extensions;
using System;

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

               return $"In the {Name} {IsOrAre} {Children.DisplayList(definiteArticle: false)}";
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

    public bool Contains<T>() where T : Object
    {
        return Children.Contains(Objects.Get<T>());
    }

    public bool Contains(Object obj)
    {
        return Children.Contains(obj);
    }
}
