using System.Linq;

namespace Adventure.Net.Actions;


//Verb 'drop' 'discard' 'throw'
//* multiheld                                 -> Drop           keys, all, all except keys  (Drop)
//* held 'at'/'against'/'on'/'onto' noun      -> ThrowAt;

// these work with non-held items at location
//* multiexcept 'in'/'into'/'down' noun       -> Insert         keys, all, all except [in/into/down] or keys down well
//* multiexcept 'on'/'onto' noun              -> PutOn          all except keys on/onto table, keys on table


public class Drop : Verb
{
    public Drop()
    {
        Name = "drop";
        Synonyms.Are("drop", "discard", "throw");
        MultiHeld = true;
    }

    // implict drop
    public bool Expects()
    {
        if (Inventory.Count == 0)
        {
            Print("What do you want to drop those things in?");
        }
        else if (Inventory.Count == 1)
        {
            // implicit drop
            var obj = Inventory.Items.Single();
            Print($"({obj.DefiniteArticle} {obj.Name})");
            return Expects(obj);
        }
        else
        {
            Print("What do you want to drop?");
        }

        return true;
    }

    public bool Expects(Object obj)
    {
        if (obj.InInventory)
        {
            obj.MoveToLocation();
            Print("Dropped.");
        }
        else if (obj.InRoom)
        {
            string isAre = obj.PluralName ? "are" : "is";
            Print($"The {obj.Name} {isAre} already here.");
        }

        return true;
    }
}
