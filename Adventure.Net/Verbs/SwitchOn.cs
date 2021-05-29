
namespace Adventure.Net.Verbs
{

    public class SwitchOn : Verb
    {
        public bool Expects(Item obj, Preposition.On on)
        {
            if (obj.IsSwitchable)
            {
                if (!obj.IsOn)
                {
                    obj.IsOn = true;
                    Print($"You switch the {obj.Name} on.");
                }
                else
                {
                    Print("That's already on.");
                }

                return true;
            }

            Print("That's not something you can switch.");
            return true;
        }

    }
}
