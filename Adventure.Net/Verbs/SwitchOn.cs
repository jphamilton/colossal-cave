using System;

namespace Adventure.Net.Verbs
{

    public class SwitchOn : Verb
    {
        public SwitchOn()
        {
            Name = "switch";
            Grammars.Add("<noun>", SwitchOnObject);
            Grammars.Add("<noun> on", SwitchOnObject);
            Grammars.Add("on <noun>", SwitchOnObject);
        }

        private bool SwitchOnObject()
        {
            if (Object.IsSwitchable && !Object.IsOn)
            {
                Object.IsOn = true;
                Print(String.Format("You switch the {0} on.", Object.Name));
            }
            else if (Object.IsOn)
            {
                Print("That's already on.");
            }
            else
            {
                Print("That's not something you can switch.");
            }

            return true;
        }
    }
}
