namespace Adventure.Net.Actions
{
    public class LookUnder : Verb
    {
        public bool Expects(Item obj)
        {
            Print("You find nothing of interest.");
            return true;
        }
    }
}
