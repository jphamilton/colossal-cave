using Adventure.Net;

namespace ColossalCave.Verbs
{
    public class Xyzzy : Verb
    {
        public Xyzzy()
        {
            Name = "xyzzy";
        }

        public bool Expects()
        {
            Print("Nothing happens.");
            return true;
        }
    }
}
