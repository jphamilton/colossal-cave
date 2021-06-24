namespace Adventure.Net.Actions
{
    public class Attack : Verb
    {
        public Attack()
        {
            Name = "attack";
            Synonyms.Are("break, crack, destroy, fight, hit, kill, murder, punch, smash, thump, torture, wreck");
        }

        public bool Expects(Object obj)
        {
            Print("Violence isn't the answer to this one");
            return true;
        }

    }
}

