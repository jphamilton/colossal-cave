using System;

namespace Adventure.Net.Verbs
{

    // TODO: implement
    public class SwitchOn : Verb
    {
        public SwitchOn()
        {
          //  Name = "switch";
          //  Grammars.Add("<noun>", SwitchOnObject);
          //  Grammars.Add("<noun> on", SwitchOnObject);
          //  Grammars.Add("on <noun>", SwitchOnObject);
        }

        // switch on lamp
        public bool Expects(Item item)
        {
            return SwitchOnObject(item);
        }

        // switch on lamp
        public bool Expects(Item item, Preposition prep)
        {
            return false;
        }

        private bool SwitchOnObject(Item obj)
        {
            if (obj.IsSwitchable && !obj.IsOn)
            {
                obj.IsOn = true;
                // TODO: use Article instead of "the"? Probably not.
                Output.Print($"You switch the {obj.Name} on.");
                return true;
            }
            else if (obj.IsOn)
            {
                Output.Print("That's already on.");
            }
            else
            {
                Output.Print("That's not something you can switch.");
            }

            return false;
        }
    }
}
