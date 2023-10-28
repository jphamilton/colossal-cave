using System;

namespace Adventure.Net.Actions;

// TODO: implement
//Verb 'wear' 'don'
//  * held                                      -> Wear;
public class Wear : Verb
{
    public Wear()
    {
        Name = "wear";
        Synonyms.Are("don");
    }

    public bool Expects([Held] Object obj)
    {
        throw new NotImplementedException("Wear not implemented");
    }
}
