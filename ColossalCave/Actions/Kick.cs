using Adventure.Net;

namespace ColossalCave.Actions
{
    public class Kick : Verb
    {
        public Kick()
        {
            Name = "kick";
        }

        public bool Expects(Object obj)
        {
            return Print("Violence isn't the answer to this one.");
        }
    }
}
