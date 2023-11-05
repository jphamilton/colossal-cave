using Adventure.Net.Extensions;

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

    public bool IsEmpty
    {
        get { return Children.Count == 0; }
    }

    public void Empty()
    {
        Children.Clear();
    }

    public bool ContentsVisible => Open || Transparent;
    
    public void Add<T>() where T : Object
    {
        Object obj = Objects.Get<T>();
        Add(obj);
    }

    public void Add(Object obj)
    {
        obj.Remove();
        obj.Parent = this;
        Children.Add(obj);
    }

    public new void Remove<T>() where T : Object
    {
        var obj = Objects.Get<T>();
        Remove(obj);
    }

    public void Remove(Object obj)
    {
        obj.Remove();
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
