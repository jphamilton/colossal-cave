using System;

namespace Adventure.Net.Verbs
{

    public class Insert : Verb
    {
        public Insert()
        {
            Name = "insert";
            Grammars.Add("<multi> in <noun>", InsertObject);
        }

        private bool InsertObject()
        {

            //if (Object.InInventory)
            //{
            //    Print(String.Format("You need to be holding the {0} before you can put it into something else.", Object.Name));
            //    return true;
            //}

            var beforeReceive = IndirectItem.Before<Receive>();
            if (beforeReceive != null)
            {
                return beforeReceive();
            }

            var c = IndirectItem as Container;
            if (c == null)
            {
                Print("That can't contain things.");
                return true;
            }

            if (!c.IsOpen)
            {
                Print(String.Format("The {0} is closed.", c.Name));
                return true;
            }

            Inventory.Remove(Item);
            c.Add(Item);

            var afterReceive = IndirectItem.After<Receive>();
            if (afterReceive != null)
            {
                return afterReceive();
            }

            Print(String.Format("You put the {0} into the {1}.", Item.Name, IndirectItem.Name));
            return true;
        }
    }
}
