namespace Adventure.Net.Actions;

public class No : Verb
{
    public No()
    {
        Name = "No";
        Synonyms.Are("no", "nope", "nah", "naw");
    }

    public bool Expects()
    {
        Print("That was a rhetorical question.");
        return true;
    }
}
