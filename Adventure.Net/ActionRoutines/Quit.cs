using Adventure.Net.Utilities;

namespace Adventure.Net.ActionRoutines;

public class Quit : Routine
{
    public Quit()
    {
        Verbs = ["quit", "q"];
        IsGameVerb = true;
    }

    public override bool Handler(Object first, Object second = null)
    {
        if (YesOrNo.Ask("Are you sure you want to quit?"))
        {
            Context.Story.IsDone = true;
        }

        return false;
    }

}
