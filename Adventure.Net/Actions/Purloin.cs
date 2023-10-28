namespace Adventure.Net.Actions;

/// <summary>
/// Helpful for testing
/// </summary>
public class Purloin : Verb
{
    public Purloin()
    {
        Name = "purloin";
        InScopeOnly = false;
    }

    public bool Expects(Object obj)
    {
        obj.Remove();
        Inventory.Add(obj);
        Print("[Purloined.]");
        return true;
    }
}
