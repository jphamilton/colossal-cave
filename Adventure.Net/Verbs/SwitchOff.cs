namespace Adventure.Net.Verbs
{
    public class SwitchOff : Verb
    {
        public bool Expects(Item obj, Preposition.Off off)
        {
            if (obj.IsSwitchable)
            {
                if (obj.IsOn)
                {
                    obj.IsOn = false;
                    Print($"You switch the {obj.Name} off.");
                }
                else
                {
                    Print("That's already off.");
                }

                return true;
            }
            
            Print("That's not something you can switch.");
            return true;
        }

    }
}
