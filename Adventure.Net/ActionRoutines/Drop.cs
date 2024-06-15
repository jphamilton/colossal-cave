using Adventure.Net.Extensions;
using Adventure.Net.Things;

namespace Adventure.Net.ActionRoutines;

//Verb 'drop' 'discard' 'throw'
//* multiheld                                 -> Drop           keys, all, all except keys  (Drop)
//* held 'at'/'against'/'on'/'onto' noun      -> ThrowAt;       

// these work with non-held items at location
//* multiexcept 'in'/'into'/'down' noun       -> Insert         keys, all, all except [in/into/down] or keys down well

//* multiexcept 'on'/'onto' noun              -> PutOn          all except keys on/onto table, keys on table

public class Drop : Routine
{
    public Drop()
    {
        Verbs = ["drop", "discard", "throw"];
        Requires = [O.MultiHeld];
        ImplicitObject = (O _) => Inventory.Items.Count == 1 ? Inventory.Items[0] : null;
    }

    private bool Dropped(Object obj)
    {
        obj.MoveToLocation();
        return Print("Dropped.");
    }

    public override bool Handler(Object obj, Object _ = null)
    {
        if (Inventory.Contains(obj))
        {
            if (obj.Parent is Player)
            {
                return Dropped(obj);
            }

            // objects in non-transparent containers are handled by the parser
            else if (obj.Parent is Container container)
            {
                if (container.Open)
                {
                    return Dropped(obj);
                }

                // object is in a transparent container in inventory that is closed.
                Print(Messages.NotHoldingThat);
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

//TODO: how to distinguish between DropOn and PutOn???

public class DropAt : Throw
{
    public DropAt()
    {
        Verbs = ["drop", "discard"];
        Prepositions = ["on", "onto", "at", "against"];
    }
}

public class DropOn : PutOnTop
{
    public DropOn()
    {
        Verbs = ["drop", "discard"];
        Prepositions = ["on", "onto", "at", "against"];
    }
}

public class DropIn : Insert
{
    public DropIn()
    {
        Verbs = ["drop", "discard"];
        Prepositions = ["in", "into", "down"];
    }
}