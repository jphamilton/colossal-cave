using Adventure.Net;

namespace ColossalCave.Objects
{
    public class Hill : Scenic
    {
        public override void  Initialize()
        {
            Name = "hill";
            Synonyms.Are("hill", "bump", "incline");
            Description = "It's just a typical hill.";
        }
    }
}


