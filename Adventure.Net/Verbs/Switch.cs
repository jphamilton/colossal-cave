namespace Adventure.Net.Verbs
{
    public class Switch : Verb
    {
        public Switch()
        {
            Name = "switch";
        }

        // <noun> on/off
        // on/off <noun>
        public bool Expects(Item item, Preposition prep)
        {
            var result = false;

            if (prep == Preposition.On)
            {
                result = Redirect<SwitchOn>(item, v => v.Expects(item));
            }
            else if (prep == Preposition.Off)
            {
                result = Redirect<SwitchOff>(item, v => v.Expects(item));
            }

            return result;
        }
    }
}
