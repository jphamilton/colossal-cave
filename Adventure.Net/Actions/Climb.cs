namespace Adventure.Net.Actions
{
    public class Climb : Verb
    {
        public Climb()
        {
            Name = "climb";
            Synonyms.Are("scale");
        }

        public bool Expects(Object obj)
        {
            return ClimbObj(obj);
        }

        public bool Expects(Preposition.Up up, Object obj)
        {
            return ClimbObj(obj);
        }

        public bool Expects(Preposition.Over over, Object obj)
        {
            return ClimbObj(obj);
        }

        private bool ClimbObj(Object obj)
        {
            Print($"Climbing {obj.ThatOrThose} would achieve little.");
            return true;
        }
    }
}
