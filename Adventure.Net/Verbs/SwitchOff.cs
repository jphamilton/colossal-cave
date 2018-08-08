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
            if (Object.IsSwitchable && Object.IsOn)
            {
                Print(String.Format("You switch the {0} off.", Object.Name));
            }
            else if (!Object.IsOn)
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
