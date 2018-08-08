using System;

namespace Adventure.Net.Verbs
{
    public class Wave : Verb
    {
        public Wave()
        {
            Grammars.Add("", WaveHands);
            Grammars.Add("<held>", WaveObject);
        }

        private bool WaveHands()
        {
            Print("You wave, feeling foolish.");
            return true;
        }
        
        private bool WaveObject()
        {
            Print(String.Format("You look ridiculous waving the {0}.", Object.Name));
            return true;
        }

        
    }
}