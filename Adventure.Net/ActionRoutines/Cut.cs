namespace Adventure.Net.ActionRoutines;

public class Cut : Routine
{
    public Cut()
    {
        Verbs = ["cut", "chop", "prune", "slice"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print($"Cutting {first.ThatOrThose} up would achieve little.");
    }
}
