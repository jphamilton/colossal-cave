using System.Linq;

namespace Adventure.Net.ActionRoutines;

/*
     Verb 'disrobe' 'doff' 'shed'
        * held                                      -> Disrobe;
 */

public class Disrobe : Routine
{
    public Disrobe()
    {
        Verbs = ["disrobe", "doff", "shed"];
        Requires = [O.Held];
        ImplicitObject = (O _) =>
        {
            var clothing = Inventory.Items.Where(x => x.Clothing && x.Worn).ToList();
            if (clothing.Count == 1)
            {
                return clothing[0];
            }

            return null;
        };
    }

    public override bool Handler(Object obj, Object _ = null)
    {
        if (obj.Clothing && obj.Worn)
        {
            obj.Worn = false;
            return Print($"You take off {obj.DName}.");
        }

        return Print("You aren't wearing that!");
    }
}


