namespace Adventure.Net.Actions
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

        public bool Expects(Object obj)
        {
            return LockObject(obj, null);
        }

        public bool Expects(Object obj, Preposition.With with, [Held]Object held)
        {
            return LockObject(obj, held);
        }

        private bool LockObject(Object obj, Object held)
        {
            if (!obj.Lockable)
            {
                Print("That doesn't seem to be something you can lock.");
            }
            else if (obj.Locked)
            {
                Print("It's locked at the moment.");
            }
            else if (obj is Door)
            {
                Door door = obj as Door;

                if (!Inventory.Contains(door.Key))
                {
                    Print("You have nothing to lock that with.");
                }
                else
                {
                    if (held == null && Inventory.Items.Count == 1)
                    {
                        Print("(with the {door.Key.Name})\n");
                    }

                    Print($"You lock the {obj.Name}.");
                    obj.Locked = true;
                }

            }

            return true;
        }
    }
}
