using System;

namespace Adventure.Net.Verbs
{
    public class SwitchOff : Verb
    {
        public SwitchOff()
        {
            Name = "switch";
            Grammars.Add("<noun> off", SwitchOffObject);
            Grammars.Add("off <noun>", SwitchOffObject);
        }

        private bool SwitchOffObject()
        {
            if (Item.IsSwitchable && Item.IsOn)
            {
                Print(String.Format("You switch the {0} off.", Item.Name));
            }
            else if (!Item.IsOn)
            {
                Print("That's already off.");
            }
            else
            {
                Print("That's not something you can switch.");
            }

            return true;
        }

    }
}
