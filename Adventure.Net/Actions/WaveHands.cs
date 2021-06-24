namespace Adventure.Net.Actions
{
    public class WaveHands : Verb
    {
        public bool Expects()
        {
            Print("You wave, feeling foolish");
            return true;
        }

        public bool Expects(Preposition.At at, Object obj)
        {
            Print($"You wave at the {obj}, feeling foolish.");
            return true;
        }
    }
}
