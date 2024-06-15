namespace Adventure.Net.ActionRoutines;

public class Search : Routine
{
    public Search()
    {
        Verbs = ["search"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object _, Object __)
    {
        return Fail("You find nothing of interest.");
    }
}
