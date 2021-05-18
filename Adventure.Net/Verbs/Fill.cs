using System;

namespace Adventure.Net.Verbs
{
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

        public bool Expects(Item obj)
        {
            throw new MissingMethodException($"{obj.Name} is missing Before<Fill> implementation");
        }

        public bool Expects(Item obj, Preposition prep, Item indirect)
        {
            throw new MissingMethodException($"{indirect.Name} is missing Before<Fill> implementation");
        }

    }
}
