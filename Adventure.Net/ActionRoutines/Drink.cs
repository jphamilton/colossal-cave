namespace Adventure.Net.ActionRoutines;

public class Drink : Routine
{
    public Drink()
    {
        Verbs = ["drink", "sip", "swallow"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object _, Object __)
    {
        return Print("You can't drink that.");
    }
}
