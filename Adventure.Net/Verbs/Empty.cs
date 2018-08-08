using System;

namespace Adventure.Net.Verbs
{
    //TODO: Verb 'empty'
    //    * noun                                      -> Empty
    //    * 'out' noun                                -> Empty
    //    * noun 'out'                                -> Empty
    //    * noun 'to'/'into'/'on'/'onto' noun         -> EmptyT;

    public class Empty : Verb
    {
        public Empty()
        {
            Name = "empty";
            Grammars.Add("<noun>", EmptyObject);
            Grammars.Add("out <noun>", EmptyObject);
            Grammars.Add("<noun> out", EmptyObject);
        }

        private bool EmptyObject()
        {
            Container obj = Object as Container;
            if (obj == null)
                Print("The " + Object.Name + " can't contain things.");
            else if (!obj.IsOpen)
                Print("The " + obj.Name + " " + obj.IsOrAre + " closed.");
            else if (obj.IsEmpty)
                Print("The " + obj.Name + " " + obj.IsOrAre + " empty already.");
            else
                Print("That would scarcely empty anything.");
            return true;
        }
    }
}

