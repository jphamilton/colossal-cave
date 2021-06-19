using Adventure.Net;

namespace ColossalCave.Actions
{
    public class Oil : Verb
    {
        public Oil()
        {
            Name = "oil";
            Synonyms.Are("grease", "lubricate");
        }

        public bool Expects(Item obj)
        {
            Print("Oil? What oil?");
            return true;
        }
    }
}
