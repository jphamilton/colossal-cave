using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Actions;

public class Plover : Routine
{
    public Plover()
    {
        Verbs = ["plover"];
    }

    public override bool Handler(Object _, Object __ = null)
    {
        return Fail("Nothing happens.");
    }
}
