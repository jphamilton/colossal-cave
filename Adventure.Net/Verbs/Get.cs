using System;
using System.Collections.Generic;

namespace Adventure.Net.Verbs
{
    // TODO: implement

    //Verb 'get'
    //* 'out'/'off'/'up'                          -> Exit
    //* multi                                     -> Take
    //* 'in'/'on'/ noun                           -> Enter
    //* 'off' noun                                -> GetOff
    //* multiinside 'from' noun                   -> Remove;

    public class Get : Verb
    {
        public Get()
        {
            Name = "get";
            Multi = true;

            //Grammars.Add("<multi>", TakeSingle);
        }

        // TODO: test
        public bool Excepts(Item obj)
        {
            return Redirect<Take>(obj, v => v.Expects(obj));

        }
    }
}
