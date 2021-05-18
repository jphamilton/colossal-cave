using System;
using System.Collections.Generic;

namespace Adventure.Net.Verbs
{
    public class Pick : Verb
    {
        public Pick()
        {
            Name = "pick up";
            Synonyms.Are("pick");
            Multi = true;
            //Grammars.Add("up <multi>", PickUpObject);
            //Grammars.Add("<multi> up", PickUpObject);
        }

        public bool Expects(Item obj, Preposition prep)
        {
            if (prep == Preposition.Up)
            {
                return Redirect<Take>(obj, v => v.Expects(obj));
            }

            // TODO: how do we handle bad prep?
            throw new NotImplementedException("pick - bad preposition");
        }

        public bool Expects(IList<Item> objects, Preposition prep)
        {
            // TODO: implement Take multi
            throw new NotImplementedException("pick <multi> up");
        }

    }
}
