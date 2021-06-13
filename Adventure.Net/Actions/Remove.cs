using System;

namespace Adventure.Net.Actions
{
/*
Verb 'remove'
    * held                                      -> Disrobe
    * multi                                     -> Take
    * multiinside 'from' noun                   -> Remove;
*/

    public class Remove : Verb
    {
        public Remove()
        {
            Name = "remove";
            //Grammars.Add(K.HELD_TOKEN, Disrobe);
           // Grammars.Add(K.MULTI_TOKEN, Take);
        }

        // TODO: not handling multi
        public bool Expects(Item obj)
        {
            if (obj.InInventory)
            {
                return Disrobe(obj);
            }

            return Redirect<Take>(obj, v => v.Expects(obj));
        }

        private bool Disrobe(Item obj)
        {
            // TODO: implement
            throw new NotImplementedException("remove <held> (disrobe)");
        }
    }
}

