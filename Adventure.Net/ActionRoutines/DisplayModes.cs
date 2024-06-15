namespace Adventure.Net.ActionRoutines;

public class Brief : Routine
{
    public Brief()
    {
        Verbs = ["brief"];
        IsGameVerb = true;
    }

    public override bool Handler(Object _, Object __)
    {
        var story = Context.Story;
        story.Verbose = false;
        return Print($"{story.Name} is now in \"brief\" mode, which gives long descriptions of places never before visited and short descriptions otherwise.");
    }
}

public class Verbose : Routine
{
    public Verbose()
    {
        Verbs = ["verbose"];
        IsGameVerb = true;
    }

    public override bool Handler(Object _, Object __)
    {
        var story = Context.Story;
        story.Verbose = true;
        return Print($"{story.Name} is now in \"verbose\" mode, which always gives long descriptions of locations (even if you've been there before).");
    }
}