namespace Adventure.Net.Actions
{
    public class Yes : Verb
    {
        public Yes()
        {
            Name = "Yes";
            Synonyms.Are("y", "yes", "yep", "yeah");
        }

        public bool Expects()
        {
            Print("That was a rhetorical question.");
            return true;
        }
    }
}
