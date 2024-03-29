﻿namespace Adventure.Net.Actions;

public class Pull : Verb
{
    public Pull()
    {
        Name = "pull";
        Synonyms.Are("drag");
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
