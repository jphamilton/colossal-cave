using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Net.ActionRoutines;

//Verb 'listen' 'hear'
//    *                                           -> Listen
//    * noun                                      -> Listen
//    * 'to' noun                                 -> Listen;
public class Listen : Routine
{
    public Listen()
    {
        Verbs = ["listen", "hear"];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print("You hear nothing unexpected");
    }
}

public class Listen2 : Listen
{
    public Listen2()
    {
        Requires = [O.Noun];
    }
}

public class ListenTo : Listen
{
    public ListenTo()
    {
        Prepositions = ["to"];
        Requires = [O.Noun];
    }
}