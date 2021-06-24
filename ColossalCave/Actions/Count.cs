using Adventure.Net;

namespace ColossalCave.Actions
{
    public class Count : Verb
    {
        public Count()
        {
            Name = "count";
        }

        public bool Expects(Object obj)
        {
            if (obj.Has("multitude"))
            {
                Print("There are a multitude.");
            }
            else
            {
                Print($"There is exactly one (1) {obj}");
            }

            return true;
        }
       
    }
}
