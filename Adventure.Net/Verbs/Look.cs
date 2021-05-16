namespace Adventure.Net.Verbs
{
    public class Look : Verb
    {
        /*
         Verb 'look' 'l//'
            *                                           -> Look
            * 'at' noun                                 -> Examine
            * 'in'/'through'/'on' noun                  -> Search
            * 'under' noun                              -> LookUnder // not implemented
            * 'up' topic 'in' noun                      -> Consult   // not implemented
            * noun=ADirection                           -> Examine
            * 'to' noun=ADirection                      -> Examine;
         */
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
            
            Library.Look(true);
            return true;
        }

        private bool ExamineObject()
        {
            return RedirectTo<Examine>("<noun>");
        }
    }
}
