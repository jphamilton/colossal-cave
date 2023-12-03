namespace Adventure.Net.Actions;

public class Verbose : Verb
{
    public Verbose()
    {
        Name = "verbose";
        GameVerb = true;
    }

    public bool Expects()
    {
        var story = Context.Story;
        story.Verbose = true;
        return Print($"{story.Name} is now in \"verbose\" mode, which always gives long descriptions of locations (even if you've been there before).");
    }
}
