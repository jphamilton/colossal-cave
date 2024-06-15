namespace Adventure.Net.ActionRoutines;

public class Yes : Routine
{
    public Yes()
    {
        Verbs = ["y", "yes", "yep", "yeah"];
    }

    public override bool Handler(Object _, Object __)
    {
        return Print("That was a rhetorical question.");
    }
}

public class No : Routine
{
    public No()
    {
        Verbs = ["no", "nope", "nah", "naw"];
    }

    public override bool Handler(Object _, Object __)
    {
        return Print("That was a rhetorical question.");
    }
}
