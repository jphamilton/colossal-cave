namespace Adventure.Net.Actions;

public class Smell : Verb
{
    public Smell()
    {
        Name = "smell";
        Synonyms.Are("sniff");
    }

    public bool Expects(Object obj)
    {
        return Print("You smell nothing unexpected.");
    }
}

