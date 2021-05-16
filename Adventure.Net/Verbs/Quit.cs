namespace Adventure.Net.Verbs
{
    public class Quit : Verb
    {
        public Quit()
        {
            Name = "quit";
            Synonyms.Are("q");
            Grammars.Add(Grammar.Empty, TryQuit);
        }

        private bool TryQuit()
        {
            
            Library.Quit();
            return true;
        }
    }
}
