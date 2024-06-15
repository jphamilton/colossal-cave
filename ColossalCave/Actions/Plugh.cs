using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Actions;

public class Plugh : Routine
{
    public Plugh()
    {
        Verbs = ["plugh"];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Fail("Nothing happens.");
    }
}
