namespace Adventure.Net.Actions
{
    public class Pull : Verb
    {
        public Pull()
        {
            Name = "pull";
            Synonyms.Are("drag");
        }

        public bool Expects(Item obj)
        {
            if (obj.IsScenery || obj.IsStatic)
            {
                Print("That is fixed in place.");
            }
            else
            {
                Print("Nothing obvious happens.");
            }

            return true;
        }
    }
}
