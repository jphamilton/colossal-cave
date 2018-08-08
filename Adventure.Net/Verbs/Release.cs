using System;

namespace Adventure.Net.Verbs
{
    public class Release : Verb
    {
        public Release()
        {
            Name = "release";
            Synonyms.Are("free");
            Grammars.Add("<noun>", ReleaseObject);
        }

        private bool ReleaseObject()
        {
            if (!Object.IsAnimate)
                Print("You can only do that to something animate");
            else
                Print("You can't release that.");
            return true;
        }
    }
}
