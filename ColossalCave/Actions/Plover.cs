using Adventure.Net;

namespace ColossalCave.Actions
{
    public class Plover : Verb
    {
        public Plover()
        {
            Name = "plover";
        }

        public bool Expects()
        {
            Print("Nothing happens.");
            return true;
        }
    }
}
