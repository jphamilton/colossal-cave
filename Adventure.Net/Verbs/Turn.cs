using System;

namespace Adventure.Net.Verbs
{
    public class Turn : Verb
    {
        // TODO: implement
        public Turn()
        {
            Name = "turn";
            Synonyms.Are("turn", "rotate", "screw", "twist", "unscrew");
            //Grammars.Add("<noun>", TurnObject);

            //Grammars.Add("<noun> on", SwitchOnObject);
            //Grammars.Add("<noun> off", SwitchOffObject);
            //Grammars.Add("on <noun>", SwitchOnObject);
            //Grammars.Add("off <noun>", SwitchOffObject);
        }

        // handles "<noun> on", "<noun> off"
        public bool Expects(Item obj, Preposition prep)
        {
            return Redirect<Switch>(obj, v => v.Expects(obj, prep));
        }

        // handled "on <noun>", "off <noun>"
        public bool Expects(Preposition prep, Item obj)
        {
            return Redirect<Switch>(obj, v => v.Expects(obj, prep));
        }

        // turn dial, screw, knob, etc.
        public bool Expects(Item item)
        {
            throw new NotImplementedException();
        }

    }
}
