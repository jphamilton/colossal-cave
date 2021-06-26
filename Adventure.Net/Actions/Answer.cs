namespace Adventure.Net.Actions
{
    public class Answer : Verb
    {
        public Answer()
        {
            Name = "answer";
            Synonyms.Are("say, shout, speak");
        }

        // TODO: Implement "topic"
        // topic 'to' creature
        public bool Expects(Object obj)
        {
            return Print("There is no reply.");
        }
    }
}

