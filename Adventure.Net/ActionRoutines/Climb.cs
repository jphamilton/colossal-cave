namespace Adventure.Net.ActionRoutines;

/*
 Verb 'climb' 'scale'
    * noun                                      -> Climb
    * 'up'/'over' noun                          -> Climb;
 */

public class Climb : Routine
{
    public Climb()
    {
        Verbs = ["climb", "scale"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        return Print($"Climbing {obj.ThatOrThose} would achieve little.");
    }
}

public class ClimbUp : Climb
{
    public ClimbUp()
    {
        Prepositions = ["up", "over"];
    }
}