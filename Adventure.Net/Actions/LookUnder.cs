namespace Adventure.Net.Actions
{
    public class LookUnder : Verb
    {
        public bool Expects(Object obj)
        {
            Print("You find nothing of interest.");
            return true;
        }
    }
}
