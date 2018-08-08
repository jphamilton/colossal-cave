namespace Adventure.Net.Verbs
{
    public class Ask : Verb
    {
        public Ask()
        {
            Name = "ask";
            //Grammars.Add("<creature> 'about' <topic>", AttackObject);
            Grammars.Add("<noun> 'about' <topic>", OnAsk);
            Grammars.Add("<noun> 'for' <noun>", OnAskFor);

        }

        private bool OnAsk()
        {
            return Default();
        }

        private bool Default()
        {
            if (!Object.IsAnimate)
            {
                Print("You can only do that to something animate.");
                return true;
            }
            Print("There was no reply.");
            return true;
        }

        private bool OnAskFor()
        {
            return Default();
        }

    }
}