using System;

namespace Adventure.Net.Actions;

//TODO: Implement Fill
//Verb 'fill'
//    * noun                                      -> Fill
//    * noun 'from' noun                          -> Fill;
public class Fill : Verb
{
    // TODO: implement
    public Fill()
    {
        Name = "fill";
    }

    public bool Expects(Object obj)
    {
        throw new MissingMethodException($"{obj.Name} is missing Before<Fill> implementation");
    }

    public bool Expects(Object obj, Preposition.From from, Object indirect)
    {
        throw new MissingMethodException($"{indirect.Name} is missing Before<Fill> implementation");
    }

}
