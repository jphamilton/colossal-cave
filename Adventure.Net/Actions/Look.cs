namespace Adventure.Net.Actions
{
    // TODO: test and implement
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
        }

        public bool Expects()
        {
            CurrentRoom.Look(true);
            return true;
        }

        public bool Expects(Item obj)
        {
            return Redirect<Examine>(obj, v => v.Expects(obj));
        }

        public bool Expects(Item obj, Preposition.At at)
        {
            return Redirect<Examine>(obj, v => v.Expects(obj));
        }
    }
}
