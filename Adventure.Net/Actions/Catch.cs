namespace Adventure.Net.Actions;

public class Catch : Verb
{
    public Catch()
    {
        Name = "catch";
        Synonyms.Are("capture");
    }

    public bool Expects(Object obj)
    {
        return YouCantCatchThat(obj);
    }

    public bool Expects(Object obj, Preposition.With with, Object indirect)
    {
        return YouCantCatchThat(obj);
    }

    private bool YouCantCatchThat(Object obj)
    {
        if (!obj.Animate)
        {
            Print("You can only do that to something animate.");
        }
        else
        {
            Print("You can't catch that.");
        }

        return true;
    }
}
