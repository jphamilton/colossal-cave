using Adventure.Net;

namespace ColossalCave.Verbs
{
    public class Xyzzy : Verb
    {
        public Xyzzy()
        {
            Name = "xyzzy";
            Grammars.Add("", OnXyzzy);
        }

        private bool OnXyzzy()
        {
            Print("Nothing happens.");
            return true;
        }
    }
}
