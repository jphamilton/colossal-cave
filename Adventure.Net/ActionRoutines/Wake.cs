namespace Adventure.Net.ActionRoutines;

//Verb 'wake' 'awake' 'awaken'
//    *                                           -> Wake
//    * 'up'                                      -> Wake
//    * creature                                  -> WakeOther
//    * creature 'up'                             -> WakeOther
//    * 'up' creature                             -> WakeOther;

public class Wake : Routine
{
    public Wake()
    {
        Verbs = ["wake", "awake", "awaken"];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Fail("The dreadful truth is, this is not a dream.");
    }
}

public class WakeUp : Wake
{
    public WakeUp()
    {
        Prepositions = ["up"];
    }
}

public class WakeOther : Routine
{
    public WakeOther()
    {
        Verbs = ["wake", "awake", "awaken"];
        Requires = [O.Animate];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Fail ("That seems unnecessary.");
    }
}
