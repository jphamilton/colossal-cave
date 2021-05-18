﻿namespace Adventure.Net.Verbs
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

        public bool Expects(Preposition p, Item obj)
        {
            if (p == Preposition.At)
            {
                return Redirect<Examine>(obj, v => v.Expects(obj));
            }

            return false;
        }
    }
}
