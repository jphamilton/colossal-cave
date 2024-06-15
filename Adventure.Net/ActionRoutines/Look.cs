using System;
using System.Linq;

namespace Adventure.Net.ActionRoutines;

//TODO: implement
/*
 Verb 'look' 'l//'
    *                                           -> Look
    * 'at' noun                                 -> Examine
    * 'in'/'through'/'on' noun                  -> Search
    * 'under' noun                              -> LookUnder 
    
    * NOT IMPLEMENTING 'up' topic 'in' noun                      -> Consult
    
    * noun=ADirection                           -> Examine
    * 'to' noun=ADirection                      -> Examine;
 */

public class Look : Routine
{
    
    public Look()
    {
        Verbs = ["look", "l"];
    }

    public override bool Handler(Object _, Object __)
    {
        CurrentRoom.Look(true);
        return true;
    }
}

public class LookIn : Search
{
    public LookIn()
    {
        Verbs = ["look", "l"];
        Prepositions = ["in", "through", "on"];
    }
}

public class LookAt : Examine
{
    public LookAt()
    {
        Verbs = ["look at"];
    }
}

public class LookUnder : Routine
{
    public LookUnder()
    {
        Verbs = ["look", "l"];
        Prepositions = ["under"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object _, Object __)
    {
        return Print("You find nothing of interest.");
    }
}

public class LookDirection : Routine
{
    public LookDirection()
    {
        Verbs = ["look", "l"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        return Print("You see nothing unexpected in that direction.");
    }
}