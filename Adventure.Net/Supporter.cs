using Adventure.Net.Extensions;

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
    
    public void Add(Object obj)
    {
        obj.Remove();
        obj.Parent = this;
        Children.Add(obj);
    }

    public bool TryDisplay(out string message)
    {
        message = null;

        if (Children.Count == 0)
        {
            return false;
        }

        var isAre = Children.Count == 1 ? "is" : "are";
        
        message = $"On {DName} {isAre} {Children.DisplayList(definiteArticle: false)}.";
        
        return true;
    }

    public bool ProvidingLight()
    {
        foreach (var supported in Children)
        {
            if (supported.Light)
            {
                return true;
            }
        }

        return false;
    }
}
