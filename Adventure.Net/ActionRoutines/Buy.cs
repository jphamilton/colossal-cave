namespace Adventure.Net.ActionRoutines;

public class Buy : Routine
{
    public Buy()
    {
        Verbs = ["buy", "purchase"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print($"Nothing is on sale.");
    }
}
