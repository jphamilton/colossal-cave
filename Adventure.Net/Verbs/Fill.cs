using System;

namespace Adventure.Net.Verbs
{
    public class Fill : Verb
    {
        public Fill()
        {
            Name = "fill";
            Grammars.Add("<noun>", FillObject);
        }

        public bool FillObject()
        {
            Print("But there's no water here to carry.");
            return true;
        }

        public bool FillObject(Object obj)
        {
            Object = obj;
            return FillObject();
        }
    }
}
