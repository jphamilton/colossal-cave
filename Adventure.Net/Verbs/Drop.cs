using System;

namespace Adventure.Net.Verbs
{

    //Verb 'drop' 'discard' 'throw'
    //* multiheld                                 -> Drop
    //* multiexcept 'in'/'into'/'down' noun       -> Insert
    //* multiexcept 'on'/'onto' noun              -> PutOn
    //* held 'at'/'against'/'on'/'onto' noun      -> ThrowAt;

    public class Drop : Verb
    {
        public Drop()
        {
            Name = "drop";
            Synonyms.Are("drop", "discard", "throw");
            Grammars.Add("<multiheld>", DropObject);
        }

        private bool DropObject()
        {
            bool result = false;

            if (Inventory.Contains(Object))
            {
                Object.MoveToLocation();
                Print("Dropped.");
                result = true;
            }
            else if (Object.AtLocation)
            {
                string isAre = Object.HasPluralName ? "are" : "is";
                Print("The {0} {1} already here.", Object.Name, isAre);
            }
            else if (Object == null)
            {
                Print("You aren't carrying anything.");
            }
            else
            {
                Print("You haven't got {0}", Object.ThatOrThose);
            }

            return result;
        }
    }
}

//Drop: switch (n) {
//        1:  if (x1 has pluralname) print (The) x1, " are "; else print (The) x1, " is ";
//            "already here.";
//        2:  "You haven't got ", (thatorthose) x1, ".";
// TODO:        3:  "(first taking ", (the) x1, " off)";
//        4:  "Dropped.";
//    }

//TODO:  Verb 'drop' 'discard' 'throw'
//171      * multiexcept 'in'/'into'/'down' noun       -> Insert
//172      * multiexcept 'on'/'onto' noun              -> PutOn
//173      * held 'at'/'against'/'on'/'onto' noun      -> ThrowAt;