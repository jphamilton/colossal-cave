using System;

namespace Adventure.Net.Verbs
{
    // TODO: implement
    /*
         Verb 'insert'
            * multiexcept 'in'/'into' noun              -> Insert;
     */
    public class Insert : Verb
    {
        public Insert()
        {
            Name = "insert";
            Multi = true;
            //Grammars.Add("<multi> in <noun>", InsertObject);
        }

        public bool Expects(Item obj, Preposition.In @in, Item indirect)
        {
            return Receive(obj, indirect);
        }

        public bool Expects(Item obj, Preposition.Into into, Item indirect)
        {
            return Receive(obj, indirect);
        }

        private bool Receive(Item obj, Item indirect)
        {
            // receive
            var receive = indirect.Receive();

            if (receive != null)
            {
                return receive(obj);
            }

            // TODO: this isn't quite right. It could be a container without a Receive
            Print("That can't contain things.");
            return true;
        }

        //private bool InsertObject()
        //{

        //    //if (Item.InInventory)
        //    //{
        //    //    Print(String.Format("You need to be holding the {0} before you can put it into something else.", Object.Name));
        //    //    return true;
        //    //}

        //    var beforeReceive = IndirectItem.Before<Receive>();
        //    if (beforeReceive != null)
        //    {
        //        return beforeReceive();
        //    }

        //    var c = IndirectItem as Container;
        //    if (c == null)
        //    {
        //        Print("That can't contain things.");
        //        return true;
        //    }

        //    if (!c.IsOpen)
        //    {
        //        Print(string.Format("The {0} is closed.", c.Name));
        //        return true;
        //    }

        //    Inventory.Remove(Item);
        //    c.Add(Item);

        //    var afterReceive = IndirectItem.After<Receive>();
        //    if (afterReceive != null)
        //    {
        //        return afterReceive();
        //    }

        //    Print(string.Format("You put the {0} into the {1}.", Item.Name, IndirectItem.Name));
        //    return true;
        //}
    }
}
