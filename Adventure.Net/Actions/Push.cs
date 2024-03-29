﻿namespace Adventure.Net.Actions;

//Verb 'push' 'clear' 'move' 'press' 'shift'
//    * noun->Push
//    * noun noun->PushDir // not implemented
//    * noun 'to' noun->Transfer; // not implemented

public class Push : Verb
{
    public Push()
    {
        Name = "push";
        Synonyms.Are("clear", "move", "press", "shift");
    }

    public bool Expects(Object obj)
    {
        if (obj.Scenery || obj.Static)
        {
            Print("That is fixed in place.");
        }
        else
        {
            Print("Nothing obvious happens.");
        }

        return true;
    }
}


