namespace Adventure.Net.Actions;

public class Drink : Verb
{
    //Verb 'drink' 'sip' 'swallow'
    //  * noun                                      -> Drink;
    public Drink()
    {
        Name = "drink";
        Synonyms.Are("sip", "swallow");
    }

    public bool Expects(Object obj)
    {
        // implement specific behavior in Before/After routines
        Print("You can't drink that.");
        return true;
    }
}
