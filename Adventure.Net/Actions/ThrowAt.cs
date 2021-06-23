namespace Adventure.Net.Actions
{
    public class ThrowAt : Verb
    {
        public ThrowAt()
        {
            // no name
        }

        public bool Expects([Held]Item obj, Preposition.At at, Item indirect)
        {
            if (indirect.Animate)
            {
                Print("You lack the nerve when it comes to the crucial moment.");
            }
            else
            {
                Print("Futile.");
            }

            return true;
        }
        
    }
}
