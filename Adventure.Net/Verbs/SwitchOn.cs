
using System.Linq;

namespace Adventure.Net.Verbs
{

    public class SwitchOn : Verb
    {
        public SwitchOn()
        {
            Synonyms.Are("on");
        }

        // implicit on
        public bool Expects()
        {
            var held = Inventory.Items.Where(o => o.IsSwitchable).ToList();
            
            if (held.Count == 1)
            {
                // this will ensure that all the before/after routines will be called for the object
                return Redirect<SwitchOn>(held[0], v => v.Expects(held[0], new Preposition.On()));
            }
            
            Print("What do you want to switch on?");
            return false;
        }

        public bool Expects(Item obj, Preposition.On on)
        {
            return On(obj);
        }

        private bool On(Item obj)
        {
            if (obj.IsSwitchable)
            {
                if (!obj.IsOn)
                {
                    obj.IsOn = true;
                    Print($"You switch the {obj.Name} on.");
                    return true;
                }
                else
                {
                    Print("That's already on.");
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
