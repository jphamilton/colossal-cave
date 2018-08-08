using System;

namespace Adventure.Net.Verbs
{
    public class Catch : Verb
    {
        public Catch()
        {
            Name = "catch";
            Synonyms.Are("capture");
            Grammars.Add("<noun>", CatchObject);
        }

        private bool CatchObject()
        {
            if (!Object.IsAnimate)
                Print("You can only do that to something animate.");
            else
                Print("You can't catch that.");
            return true;
        }
    }
}
