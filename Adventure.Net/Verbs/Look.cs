namespace Adventure.Net.Verbs
{
    public class Look : Verb
    {
        public Look()
        {
            Name = "look";
            Synonyms.Are("l");
            Grammars.Add(Grammar.Empty, LookAtRoom);
            Grammars.Add("at <noun>", ExamineObject);
            Grammars.Add("<noun>", ExamineObject);
        }

        private bool LookAtRoom()
        {
            Library L = new Library();
            L.Look(true);
            return true;
        }

        private bool ExamineObject()
        {
            return RedirectTo<Examine>("<noun>");
        }
    }
}
