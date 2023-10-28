namespace Adventure.Net.Actions;

public class Rub : Verb
{
    public Rub()
    {
        Name = "rub";
        Synonyms.Are("rub", "clean", "dust", "polish", "scrub", "shine", "sweep", "wipe");
    }

    public bool Expects(Object obj)
    {
        Print("You achieve nothing by this.");
        return true;
    }
}
