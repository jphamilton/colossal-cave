﻿using System.Linq;

namespace Adventure.Net.Verbs
{
    public class SwitchOff : Verb
    {
        public SwitchOff()
        {
            Synonyms.Are("off");
        }

        public bool Expects()
        {
            var held = Inventory.Items.Where(o => o.IsSwitchable).ToList();

            if (held.Count == 1)
            {
                // this will ensure that all the before/after routines will be called for the object
                return Redirect<SwitchOff>(held[0], v => v.Expects(held[0], new Preposition.Off()));
            }

            Print("What do you want to switch off?");
            return false;
        }

        public bool Expects(Item obj, Preposition.Off off)
        {
            return Off(obj);
        }

        private bool Off(Item obj)
        {
            if (obj.IsSwitchable)
            {
                if (obj.IsOn)
                {
                    obj.IsOn = false;
                    Print($"You switch the {obj.Name} off.");
                    return true;
                }
                else
                {
                    Print("That's already off.");
                }
            }
            else
            {
                Print("That's not something you can switch.");
            }

            return false;
        }
    }
}
