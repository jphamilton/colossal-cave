namespace Adventure.Net.ActionRoutines;

public class Release : Routine
{
    public Release()
    {
        Verbs = ["release", "free"];
        Requires = [O.Animate];
    }

    public override bool Handler(Object obj, Object _ = null)
    {
        return Fail("You can't release that.");
    }
}
