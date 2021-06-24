using System.Linq;

namespace Adventure.Net.Actions
{
    public class SwitchOff : Verb
    {
        public SwitchOff()
        {
            Synonyms.Are("off");
        }

        public bool Expects()
        {
            var held = Inventory.Items.Where(o => o.Switchable).ToList();

            if (held.Count == 1)
            {
                // this will ensure that all the before/after routines will be called for the object
                return Redirect<SwitchOff>(held[0], v => v.Expects(held[0], new Preposition.Off()));
            }

            Print("What do you want to switch off?");
            return false;
        }

        public bool Expects(Object obj, Preposition.Off off)
        {
            return Off(obj);
        }

        private bool Off(Object obj)
        {
            if (obj.Switchable)
            {
                if (obj.On)
                {
                    obj.On = false;
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
