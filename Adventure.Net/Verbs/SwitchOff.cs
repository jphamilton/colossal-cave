using System;

namespace Adventure.Net.Verbs
{
    // TODO: implement
    public class SwitchOff : Verb
    {
        public SwitchOff()
        {
           // Name = "switch";
           // Grammars.Add("<noun> off", SwitchOffObject);
           // Grammars.Add("off <noun>", SwitchOffObject);
        }

        // <noun> off
        // off <noun>

        public bool Expects(Item item)
        {
            return false;
        }

        // <noun> off
        // off <noun>
        public bool Expects(Item item, Preposition prep)
        {
            if (prep == Preposition.On)
            {

            }
            else if (prep == Preposition.Off)
            {

            }
            else
            {
                // would say "switch" instead of "turn
                // I only understand you as far as wanting to "turn" "the" "brass lantern"
            }

            return false;
        }

        //private bool SwitchOffObject()
        //{
        //    if (Item.IsSwitchable && Item.IsOn)
        //    {
        //        Print(String.Format("You switch the {0} off.", Item.Name));
        //    }
        //    else if (!Item.IsOn)
        //    {
        //        Print("That's already off.");
        //    }
        //    else
        //    {
        //        Print("That's not something you can switch.");
        //    }

        //    return true;
        //}

    }
}
