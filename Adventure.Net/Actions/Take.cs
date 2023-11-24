using System.Linq;

namespace Adventure.Net.Actions;

public class Take : Verb
{
    /*
    Verb 'take' 'carry' 'hold'
        * multi                                     -> Take
        * 'off' <held>                              -> Disrobe
        * multiinside 'from'/'off' noun             -> Remove  // TODO: not implemented
        * 'inventory'                               -> Inv;    // TODO: not implemented - not possible with current parser
    */
    public Take()
    {
        Name = "take";
        Synonyms.Are("take", "carry", "hold");
        Multi = true;
    }

    public bool Expects()
    {
        var objects =
            (from o in CurrentRoom.ObjectsInRoom()
             where !o.Static && !o.Scenery
             select o).ToList();

        if (objects.Count == 1)
        {
            // TODO: Can this be replaced with ImplicitTake?
            // implicit take
            var obj = objects.Single();
            Print($"({obj.DefiniteArticle} {obj.Name})");
            return Expects(obj);
        }

        return Print("What do you want to take?");
    }

    public bool Expects(Object obj)
    {
        if (obj.Scenery)
        {
            Print("That's hardly portable.");
        }
        else if (obj.Static)
        {
            Print("That's fixed in place.");
        }
        else if (obj.Animate)
        {
            Print($"I don't suppose the {obj.Name} would care for that.");
        }
        else if (Inventory.Contains(obj))
        {
            Print("You already have that.");
        }
        else
        {
            // TODO: No way to change Inventory rules like this - this probably needs to be moved to InventoryRoot maybe?

            if (Inventory.Count >= 8)
            {
                Print("You're carrying too many things already.");
                return false;
            }

            Inventory.Add(obj);
            Print("Taken.");
        }

        return true;
    }

    public bool Expects([Held]Object obj, Preposition.Off off)
    {
        return Redirect<Disrobe>(x => x.Expects(obj));
    }

}
