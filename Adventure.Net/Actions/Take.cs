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
             where !o.Absent && !o.Static && !o.Scenery
             select o).ToList();

        if (objects.Count == 1)
        {
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
            if (Inventory.CanAdd())
            {
                Inventory.Add(obj);
                return Print("Taken.");
            }

            Print(Inventory.CarryingTooMuch());
            return false;
        }

        return true;
    }

    public bool Expects([Held]Object obj, Preposition.Off off)
    {
        return Redirect<Disrobe>(x => x.Expects(obj));
    }

}
