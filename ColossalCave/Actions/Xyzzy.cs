using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Actions;

public class Xyzzy : Routine
{
    public Xyzzy()
    {
        Verbs = ["xyzzy"];
    }

    public override bool Handler(Object _, Object __ = null)
    {
        return Fail("Nothing happens.");
    }
}
