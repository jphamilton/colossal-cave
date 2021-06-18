namespace Adventure.Net.Actions
{
    public class Search : Verb
    {
        public Search()
        {
            Name = "search";
        }

        public bool Expects(Item obj)
        {
            Print("You find nothing of interest.");
            return true;
        }
    }
}
