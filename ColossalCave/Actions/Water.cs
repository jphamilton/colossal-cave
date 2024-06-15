using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Actions;

public class Water : Routine
{
    public Water()
    {
        Verbs = ["water"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _ = null)
    {
        return true;
    }
}
