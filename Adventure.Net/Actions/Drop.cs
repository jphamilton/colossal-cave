using System.Linq;

namespace Adventure.Net.Actions
{

    //Verb 'drop' 'discard' 'throw'
    //* multiheld                                 -> Drop           keys, all, all except keys  (Drop)
    //* multiexcept 'in'/'into'/'down' noun       -> Insert         keys, all, all except [in/into/down] or keys down well
    //* multiexcept 'on'/'onto' noun              -> PutOn          all except keys on/onto table, keys on table
    //* held 'at'/'against'/'on'/'onto' noun      -> ThrowAt;

    
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
                Print($"({obj.Article} {obj.Name})");
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

        //private bool DropObject()
        //{
        //    bool result = false;

        //    if (Inventory.Contains(Item))
        //    {
        //        Item.MoveToLocation();
        //        Print("Dropped.");
        //        result = true;
        //    }
        //    else if (Item == null)
        //    {
        //        Print("You aren't carrying anything.");
        //        //Print("You aren't carrying anything.");
        //    }
        //    else if (Item.AtLocation)
        //    {
        //        string isAre = Item.HasPluralName ? "are" : "is";
        //        Print("The {0} {1} already here.", Item.Name, isAre);
        //    }
        //    else
        //    {
        //        Print("You haven't got {0}", Item.ThatOrThose);
        //    }

        //    return result;
        //}
    }
}


