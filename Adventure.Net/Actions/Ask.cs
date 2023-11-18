namespace Adventure.Net.Actions;

public class Ask : Verb
{
    public Ask()
    {
        Name = "ask";
    }

    public bool Expects(Object obj, Preposition.About about, Object indirect)
    {
        if (!obj.Animate)
        {
            return Print("You can only do that to something animate.");
        }

        return Print("There was no reply.");
    }

}