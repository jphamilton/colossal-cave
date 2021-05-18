using System;

namespace Adventure.Net.Verbs
{

    // TODO: implement
    public class Lock : Verb
    {
        public Lock()
        {
            Name = "lock";
           // Grammars.Add("<noun>", LockObject);
           // Grammars.Add("<noun> with <held>", LockObject);
        }

        //private bool LockObject()
        //{
        //    if (!Item.IsLockable)
        //    {
        //        Print("That doesn't seem to be something you can lock.");
        //    }
        //    else if (Item.IsLocked)
        //    {
        //        Print("It's locked at the moment.");
        //    }
        //    else if (Item is Door)
        //    {
        //        Door door = Item as Door;
        //        if (!Inventory.Contains(door.Key))
        //            Print("You have nothing to lock that with.");
        //        else
        //        {
        //            if (IndirectItem == null && Inventory.Items.Count == 1)
        //                Print(String.Format("(with the {0})\n", door.Key.Name));
        //            Print(String.Format("You lock the {0}.", Item.Name));
        //            Item.IsLocked = true;
        //        }
                
        //    }

        //    return true;
        //}
    }
}
