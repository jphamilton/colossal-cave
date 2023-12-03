namespace Adventure.Net.Actions;

public class Brief : Verb
{
    public Brief()
    {
        Name = "brief";
        GameVerb = true;
    }

    public bool Expects()
    {
        var story = Context.Story;
        story.Verbose = false;
        return Print($"{story.Name} is now in \"brief\" mode, which gives long descriptions of places never before visited and short descriptions otherwise.");
    }
}
