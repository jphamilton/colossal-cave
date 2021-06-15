namespace Adventure.Net.Actions
{
    public class Climb : Verb
    {
        public Climb()
        {
            Name = "climb";
            Synonyms.Are("scale");
        }

        public bool Expects(Item obj)
        {
            return ClimbObj(obj);
        }

        public bool Expects(Preposition.Up up, Item obj)
        {
            return ClimbObj(obj);
        }

        public bool Expects(Preposition.Over over, Item obj)
        {
            return ClimbObj(obj);
        }

        private bool ClimbObj(Item obj)
        {
            Print($"Climbing {obj.ThatOrThose} would achieve little.");
            return true;
        }
    }
}
