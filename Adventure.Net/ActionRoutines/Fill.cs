using System;

namespace Adventure.Net.ActionRoutines;

public class Fill : Routine
{
    public Fill()
    {
        Verbs = ["fill"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object first, Object _)
    {
        return Print($"There isn't anything obvious with which to fill {first.DName}.");
    }

}

public class FillFrom : Routine
{
    public FillFrom()
    {
        Verbs = ["fill"];
        Prepositions = ["from"];
        Requires = [O.Noun, O.Noun];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print($"Filling {first.DName} from {second.DName} wouldn't make much sense.");
    }
}
