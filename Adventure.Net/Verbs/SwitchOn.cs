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
            if (Item.IsSwitchable && !Item.IsOn)
            {
                Item.IsOn = true;
                Print(String.Format("You switch the {0} on.", Item.Name));
            }
            else if (Item.IsOn)
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
