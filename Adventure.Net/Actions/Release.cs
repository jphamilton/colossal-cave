namespace Adventure.Net.Actions;

public class Release : Verb
{
    public Release()
    {
        Name = "release";
        Synonyms.Are("free");
    }

    public bool Expects(Object obj)
    {
        // whatever is being released must provide a Before<Release> routine
        if (obj.Animate)
        {
            Print("You can't release that.");
        }
        else
        {
            Print("You can only do that to something animate.");
        }

        return true;
    }
}
