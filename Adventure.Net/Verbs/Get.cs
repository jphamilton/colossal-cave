using System;

namespace Adventure.Net.Verbs
{

    //Verb 'get'
    //* 'out'/'off'/'up'                          -> Exit
    //* multi                                     -> Take
    //* 'in'/'into'/'on'/'onto' noun              -> Enter
    //* 'off' noun                                -> GetOff
    //* multiinside 'from' noun                   -> Remove;

    public class Get : Verb
    {
        public Get()
        {
            Name = "get";   
            Grammars.Add("<multi>", TakeSingle);
        }

        private bool TakeSingle()
        {
            return RedirectTo<Take>(K.MULTI_TOKEN);
        }
    }
}
