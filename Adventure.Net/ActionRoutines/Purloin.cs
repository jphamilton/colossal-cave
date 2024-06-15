namespace Adventure.Net.ActionRoutines;

public class Purloin : Routine
{
    public Purloin()
    {
        Verbs = ["purloin"];
        InScopeOnly = false;
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        Inventory.Add(obj);
        Print("[Purloined.]");
        return true;
    }
}
