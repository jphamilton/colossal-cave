using Adventure.Net.Extensions;

namespace Adventure.Net.ActionRoutines;

//Verb 'lock'
//    * noun 'with' held -> Lock;

public class Lock : Routine
{
    public Lock()
    {
        Verbs = ["lock"];
        Requires = [O.Noun, O.Held];
        Prepositions = ["with"];
        ImplicitObject = (O _) => Inventory.Count == 1 ? Inventory.Items[0] : null;
    }

    public override bool Handler(Object obj, Object held)
    {
        if (!obj.Lockable)
        {
            return Fail("That doesn't seem to be something you can lock.");
        }
        else if (obj.Locked)
        {
            return Fail("It's locked at the moment.");
        }
        else if (obj.Lockable)
        {
            if (obj.Key != held)
            {
                return Fail($"{held.DName.Capitalize()} doesn't seem to fit the lock.");
            }
            
            obj.Locked = true;
            
            return Success($"You lock {obj.DName}.");
        }

        return false;
    }
}
