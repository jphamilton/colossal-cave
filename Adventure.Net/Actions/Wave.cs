
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
            return Redirect<WaveHands>(v => v.Expects());
        }

        public bool Expects(Preposition.At at, Object obj)
        {
            return Redirect<WaveHands>(obj, v => v.Expects(at, obj));
        }

        public bool Expects(Object obj)
        {
            Print($"You look ridiculous waving the {obj}.");
            return true;
        }

        public bool Expects(Object obj, Preposition.At at, Object indirect)
        {
            Print($"You wave the {obj} at the {indirect}, feeling foolish.");
            return true;
        }
    }
}