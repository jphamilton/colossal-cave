namespace Adventure.Net.Verbs
{
    public class Go : Verb
    {
        public Go()
        {
            Name = "go";
            Synonyms.Are("g", "walk", "run");
            Grammars.Add(Grammar.Empty,VagueGo);
            Grammars.Add(K.DIRECTION_TOKEN, ()=> false);
            Grammars.Add(K.NOUN_TOKEN, EnterIt);
        }

        private bool VagueGo()
        {
            Print("You'll have to say which compass direction to go in.");
            return true;
        }

        private bool EnterIt()
        {
            return RedirectTo<Enter>(K.NOUN_TOKEN);
        }
        

    }
}
