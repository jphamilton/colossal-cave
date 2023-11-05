using Adventure.Net.Extensions;
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

    // dropping requires the object to be in inventory, but
    // using [Held] attribute will not work here. Would lead to this:
    // > drop bottle
    // (first taking the small bottle)
    // Dropped.
    public bool Expects(Object obj)
    {
        bool Dropped()
        {
            obj.MoveToLocation();
            return Print("Dropped.");
        }

        if (obj.InInventory)
        {
            if (obj.Parent is InventoryRoot)
            {
                return Dropped();
            }

            // objects in non-transparent containers are handled by the parser
            else if (obj.Parent is Container container)
            {
                if (container.Open)
                {
                    // For now, a special case where we have to do work that should be done in the parser
                    var implicitTake = new ImplicitTake(obj);
                    implicitTake.Invoke();

                    return Dropped();
                }

                // object is in a transparent container in inventory that is closed.
                Print("You aren't holding that!");
            }
        }
        else if (obj.InRoom)
        {
            string isAre = obj.PluralName ? "are" : "is";
            Print($"{obj.DefiniteArticle.Capitalize()} {obj.Name} {isAre} already here.");
        }

        return true;
    }
}
