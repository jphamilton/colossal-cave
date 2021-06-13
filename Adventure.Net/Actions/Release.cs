using System;

namespace Adventure.Net.Actions
{
    //Verb 'release' 'free'
    //    * creature              -> Release;
    public class Release : Verb
    {
        public Release()
        {
            Name = "release";
            Synonyms.Are("free");
        }

        public bool Expects(Item obj)
        {
            // whatever is being released must provide a Before<Release> routine
            if (obj.IsAnimate)
            {
                Print("You can't release that.");
            }
            else
            {
                Print("You can only do that to something animate");
            }

            return true;
        }
    }
}
