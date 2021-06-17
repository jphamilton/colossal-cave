using Adventure.Net;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class Hill : Scenic
    {
        public override void  Initialize()
        {
            Name = "hill";
            Synonyms.Are("hill", "bump", "incline");
            Description = "It's just a typical hill.";

            FoundIn<HillInRoad>();
        }
    }
}


