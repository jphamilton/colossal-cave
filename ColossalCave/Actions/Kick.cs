using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Actions;

public class Kick : Routine
{
    public Kick()
    {
        Verbs = ["kick"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        return Fail("Violence isn't the answer to this one.");
    }
}
