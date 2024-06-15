using Adventure.Net.Extensions;

namespace Adventure.Net.ActionRoutines;

//Verb 'unlock'
//    * noun 'with' held -> Unlock;

public class Unlock : Routine
{
    public Unlock()
    {
        Verbs = ["unlock"];
        Prepositions = ["with"];
        Requires = [O.Noun, O.Held];
        ImplicitObject = (O _) => Inventory.Items.Count == 1 ? Inventory.Items[0] : null;
    }

    public override bool Handler(Object obj, Object indirect)
    {
        if (!obj.Lockable)
        {
            return Fail($"{obj.DName.Capitalize()} doesn't seem to be something you can unlock.");
        }

        var key = obj.Key;

        if (indirect != null && indirect != key)
        {
            return Fail($"{indirect.DName.Capitalize()} doesn't seem to fit the lock.");
        }
        else if (indirect != null && indirect == key && Inventory.Contains(indirect))
        {
            if (obj.Locked)
            {
                obj.Locked = false;
                return Success($"You unlock {obj.DName}.");
            }
            else
            {
                return Fail("That's unlocked at the moment.");
            }
        }
        
        return false;
    }
}
