using Adventure.Net;
using Adventure.Net.ActionRoutines;
using ColossalCave.Things;

namespace ColossalCave.Actions;

public class On : Routine
{
    public On()
    {
        Verbs = ["on"];
        Requires = [];
    }

    public override bool Handler(Object _, Object __ = null)
    {
        var lamp = Objects.Get<BrassLantern>();
        
        if (Inventory.Contains(lamp))
        {
            return Redirect.To<SwitchOn>(lamp);
        }

        return Fail("You have no lamp");
    }
}

public class Off : Routine
{
    public Off()
    {
        Verbs = ["off"];
        Requires = [];
    }

    public override bool Handler(Object _, Object __ = null)
    {
        var lamp = Objects.Get<BrassLantern>();

        if (Inventory.Contains(lamp))
        {
            return Redirect.To<SwitchOff>(lamp);
        }

        return Fail("You have no lamp");
    }
}
