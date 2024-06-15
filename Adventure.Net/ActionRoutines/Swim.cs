namespace Adventure.Net.ActionRoutines;

public class Swim : Routine
{
    public Swim()
    {
        Verbs = ["swim", "dive"];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Fail("There's not enough water to swim in.");
    }
}
