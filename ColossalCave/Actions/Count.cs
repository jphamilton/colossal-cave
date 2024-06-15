using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Actions;

public class Count : Routine
{
    public Count()
    {
        Verbs = ["count"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        if (obj.Multitude)
        {
            return Print("There are a multitude.");
        }
        
        return Print($"There is exactly one (1) {obj}");
    }

}
