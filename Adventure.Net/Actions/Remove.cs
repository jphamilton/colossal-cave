﻿using System;

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

        public bool Expects(Object obj)
        {
            if (obj.InInventory)
            {
                return Disrobe(obj);
            }

            return Redirect<Take>(obj, v => v.Expects(obj));
        }

        private bool Disrobe(Object obj)
        {
            throw new NotImplementedException("remove <held> (disrobe)");
        }
    }
}

