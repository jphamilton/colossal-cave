namespace Adventure.Net.ActionRoutines;

//Verb 'catch' 'capture'
//    * creature              -> Catch
//    * creature 'with' held  -> Catch;

public class Catch : Routine
{
    public Catch()
    {
        Verbs = ["catch", "capture"];
        Requires = [O.Animate];
    }

    public override bool Handler(Object creature, Object _)
    {
        return Print("You can't catch that.");
    }
}

public class CatchWith : Routine
{
    public CatchWith()
    {
        Verbs = ["catch", "capture"];
        Prepositions = ["with"];
        Requires = [O.Animate, O.Held];
    }

    public override bool Handler(Object creature, Object held)
    {
        return Print("You can't catch that.");
    }
}
