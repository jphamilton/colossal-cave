using System;

namespace Adventure.Net.Verbs
{

    public class Lock : Verb
    {
        public Lock()
        {
            Name = "lock";
            Grammars.Add("<noun>", LockObject);
            Grammars.Add("<noun> with <held>", LockObject);
        }

        private bool LockObject()
        {
            if (!Object.IsLockable)
            {
                Print("That doesn't seem to be something you can lock.");
            }
            else if (Object.IsLocked)
            {
                Print("It's locked at the moment.");
            }
            else if (Object is Door)
            {
                Door door = Object as Door;
                if (!Inventory.Contains(door.Key))
                    Print("You have nothing to lock that with.");
                else
                {
                    if (IndirectObject == null && Inventory.Items.Count == 1)
                        Print(String.Format("(with the {0})\n", door.Key.Name));
                    Print(String.Format("You lock the {0}.", Object.Name));
                    Object.IsLocked = true;
                }
                
            }

            return true;
        }
    }
}
