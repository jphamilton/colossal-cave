namespace Adventure.Net.Actions;

public class Drink : Verb
{
    public Drink()
    {
        Name = "drink";
        Synonyms.Are("sip", "swallow");
    }

    public bool Expects(Object obj)
    {
        return Print("You can't drink that.");
    }
}
