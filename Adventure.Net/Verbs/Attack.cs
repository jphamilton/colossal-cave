namespace Adventure.Net.Verbs
{
    public class Attack : Verb
    {
        public Attack()
        {
            Name = "attack";
            Synonyms.Are("break, crack, destroy, fight, hit, kill, murder, punch, smash, thump, torture, wreck");
            Grammars.Add(K.NOUN_TOKEN, AttackObject);
        }

        private bool AttackObject()
        {
            Print("Violence isn't the answer to this one");
            return true;
        }

    }
}

