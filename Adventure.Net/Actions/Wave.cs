
namespace Adventure.Net.Actions
{
    public class Wave : Verb
    {
        public Wave()
        {
            Name = "wave";
        }

        public bool Expects()
        {
            Print("You wave, feeling foolish.");
            return true;
        }

        public bool Expects(Item obj)
        {
            Print($"You look ridiculous waving {obj}.");
            return true;
        }

    }
}