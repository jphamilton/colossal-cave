using System;

namespace Adventure.Net.Actions;

/*
Verb 'remove'
* held                                      -> Disrobe
* multi                                     -> Take
* multiinside 'from' noun                   -> Remove; // TODO: Implement e.g. "remove bird from cage"
*/

public class Remove : Verb
{
    public Remove()
    {
        Name = "remove";
    }

    public bool Expects(Object obj)
    {
        if (Inventory.Contains(obj) && obj.Clothing && obj.Worn)
        {
            return Redirect<Disrobe>(x => x.Expects(obj));
        }

        return Redirect<Take>(obj, v => v.Expects(obj));
    }

}

