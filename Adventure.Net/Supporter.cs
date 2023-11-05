namespace Adventure.Net;


/// <summary>
/// Object is table-like, you can put things on it.
/// </summary>
public abstract class Supporter : Object
{
    public Supporter()
    {
        Scenery = true;
        Static = true;
    }

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
}
