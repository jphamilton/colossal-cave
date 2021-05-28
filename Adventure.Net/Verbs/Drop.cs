namespace Adventure.Net.Verbs
{

    //Verb 'drop' 'discard' 'throw'
    //* multiheld                                 -> Drop
    //* multiexcept 'in'/'into'/'down' noun       -> Insert
    //* multiexcept 'on'/'onto' noun              -> PutOn
    //* held 'at'/'against'/'on'/'onto' noun      -> ThrowAt;

    // TODO: implement Insert, PutOn, ThrowAt
    public class Drop : Verb
    {
        public Drop()
        {
            Name = "drop";
            Synonyms.Are("drop", "discard", "throw");
            MultiHeld = true;
        }

        public bool Expects()
        {
            Print("You aren't carrying anything.");
            return true;
        }

        public bool Expects(Item obj)
        {
            if (obj.InInventory)
            {
                obj.MoveToLocation();
                Print("Dropped.");
            }
            else if (obj.InRoom)
            {
                string isAre = obj.HasPluralName ? "are" : "is";
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

//Drop: switch (n) {
//        1:  if (x1 has pluralname) print (The) x1, " are "; else print (The) x1, " is ";
//            "already here.";
//        2:  "You haven't got ", (thatorthose) x1, ".";
// TODO:        3:  "(first taking ", (the) x1, " off)";
//        4:  "Dropped.";
//    }

