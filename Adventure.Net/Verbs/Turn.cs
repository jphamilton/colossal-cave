using System;

namespace Adventure.Net.Verbs
{
    public class Turn : Verb
    {
        public Turn()
        {
            Name = "turn";
            Synonyms.Are("turn", "rotate", "screw", "twist", "unscrew");
            Grammars.Add("<noun>", TurnObject);
            Grammars.Add("<noun> on", SwitchOnObject);
            Grammars.Add("<noun> off", SwitchOffObject);
            Grammars.Add("on <noun>", SwitchOnObject);
            Grammars.Add("off <noun>", SwitchOffObject);
        }

        private bool SwitchOnObject()
        {
            return RedirectTo<SwitchOn>("on <noun>");
        }
        
        private bool SwitchOffObject()
        {
            return RedirectTo<SwitchOff>("off <noun>");
        }
        
        private bool TurnObject()
        {
            throw new Exception("This is not implemented");
        }
    }
}
