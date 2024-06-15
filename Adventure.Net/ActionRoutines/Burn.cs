namespace Adventure.Net.ActionRoutines;

public class Burn : Routine
{
    public Burn()
    {
        Verbs = ["burn"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print($"This dangerous act would achieve little.");
    }
}

public class BurnWith : Routine
{
    public BurnWith()
    {
        Verbs = ["burn"];
        Prepositions = ["with"];
        Requires = [O.Noun, O.Held];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print($"This dangerous act would achieve little.");
    }
}
