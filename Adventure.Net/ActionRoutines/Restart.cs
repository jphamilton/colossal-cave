using Adventure.Net.Utilities;

namespace Adventure.Net.ActionRoutines;

public class Restart : Routine
{
    public Restart()
    {
        Verbs = ["restart"];
        IsGameVerb = true;
    }

    public override bool Handler(Object first, Object second = null)
    {
        if (YesOrNo.Ask("Are you sure you want to restart?"))
        {
            Context.Story.Initialize();
            return true;
        }

        return false;
    }
}
