using Adventure.Net.Extensions;

namespace Adventure.Net.ActionRoutines;

public class Eat : Routine
{
    public Eat()
    {
        Verbs = ["eat"];
        Requires = [O.Held];
    }

    public override bool Handler(Object obj, Object _ = null)
    {
        if (obj.Edible)
        {
            Print($"You eat {obj.DName}.");
            obj.Remove();
            return true;
        }
        
        return Fail($"{obj.TheyreOrThats.Capitalize()} plainly inedible.");
    }
}