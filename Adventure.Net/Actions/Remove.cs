using System;

namespace Adventure.Net.Actions;

/*
Verb 'remove'
* held                                      -> Disrobe
* multi                                     -> Take
* multiinside 'from' noun                   -> Remove; // TODO
*/

public class Remove : Verb
{
    public Remove()
    {
        Name = "remove";
    }

    public bool Expects(Object obj)
    {
        if (obj.InInventory && obj.Clothing && obj.Worn)
        {
            return Disrobe(obj);
        }

        return Redirect<Take>(obj, v => v.Expects(obj));
    }

    private bool Disrobe(Object obj)
    {
        obj.Worn = false;
        return Print($"You take off {obj.DefiniteArticle} {obj.Name}.");
    }
}

