using System;

namespace Adventure.Net.Verbs
{
    public class Count : Verb
    {
        public Count()
        {
            Name = "count";
            Grammars.Add("<noun>", CountObject);
        }

        private bool CountObject()
        {
            throw new Exception("Counting has not been implemented yet.");
        }
    }
}
