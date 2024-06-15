using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Actions;

public class Oil : Routine
{
    public Oil()
    {
        Verbs = ["oil", "grease", "lubricate", "lube"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        return Fail("Oil? What oil?");
    }
}
