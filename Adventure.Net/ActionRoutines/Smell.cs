namespace Adventure.Net.ActionRoutines;

public class Smell : Routine
{
    public Smell()
    {
        Verbs = ["smell", "sniff"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object _, Object __)
    {
        return Fail("You smell nothing unexpected.");
    }
}
