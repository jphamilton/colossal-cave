namespace Adventure.Net.Actions
{
    public class Unlock : Verb
    {
        public Unlock()
        {
            Name = "unlock";
        }

       
        // implicit unlock
        // if the key is in inventory and is the only item, the
        public bool Expects(Object obj)
        {
            return UnlockObject(obj);
            //if (!obj.IsLockable)
            //{
            //    Print("That doesn't seem to be something you can unlock.");
            //}
            //else if (!obj.IsLocked)
            //{
            //    Print("It's unlocked at the moment.");
            //}



        }

        public bool Expects(Object obj, Preposition.With with, [Held]Object indirect)
        {
            return UnlockObject(obj, indirect);
        }

        private bool UnlockObject(Object obj, Object indirect = null)
        {
            var key = obj.Key;

            if (indirect == null)
            {
                // implicit unlock
                if (key != null && key.InInventory && Inventory.Count == 1)
                {
                    Print($"(with the {key.Name})");
                }
                else
                {
                    Print($"What do you want to unlock {obj.Article} {obj.Name} with?");
                    return true;
                }
            }

            if (indirect != null && indirect != key)
            {
                Print("That doesn't seem to fit the lock.");
                return true;
            }

            if (key != null && key.InInventory)
            {
                if (obj.Locked)
                {
                    Print($"You unlock the {obj.Name}.");
                    obj.Locked = false;
                }
                else
                {
                    Print("That's unlocked at the moment.");
                }

                return true;
            }

            Print("You have nothing to unlock that with.");
            return true;
        }
        
    }
}
