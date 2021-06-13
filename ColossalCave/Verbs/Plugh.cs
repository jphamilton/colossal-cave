using Adventure.Net;

namespace ColossalCave.Verbs
{
    public class Plugh : Verb
    {
        public Plugh()
        {
            Name = "plugh";
        }

        public bool Expects()
        {
            Print("Nothing happens.");
            return true;
        }
    }
}
