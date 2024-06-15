using System.Linq;

namespace Adventure.Net.ActionRoutines;

public class SwitchOn : Routine
{
    public SwitchOn()
    {
        Verbs = ["switch", "on", "turn", "rotate", "screw", "twist", "unscrew"];
        Prepositions = ["on"];
        Requires = [O.Noun];
        ImplicitObject = (O _) =>
        {
            var held = Inventory.Items.Where(o => o.Switchable).ToList();
            return held.Count == 1 ? held[0] : null;
        };
    }

    public override bool Handler(Object obj, Object _ = null)
    {
        if (obj.Switchable)
        {
            if (!obj.On)
            {
                obj.On = true;
                return Print($"You switch {obj.DName} on.");
            }
            else
            {
                return Fail("That's already on.");
            }
        }
        
        return Fail($"That's not something you can switch on.");
    }
}
