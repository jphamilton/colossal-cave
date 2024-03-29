﻿namespace Adventure.Net.Actions;

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

    public bool Expects(Object obj)
    {
        return Redirect<Take>(obj, v => v.Expects(obj));

    }
}
