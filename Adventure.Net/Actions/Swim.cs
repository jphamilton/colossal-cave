namespace Adventure.Net.Actions;

public class Swim : Verb
{
    public Swim()
    {
        Name = "swim";
        Synonyms.Are("dive");
    }

    public bool Expects()
    {
        Print("There's not enough water to swim in.");
        return true;
    }
}