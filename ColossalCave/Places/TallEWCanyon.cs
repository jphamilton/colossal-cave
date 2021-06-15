using Adventure.Net;

namespace ColossalCave.Places
{
    public class TallEWCanyon : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Tall E/W Canyon";
            Synonyms.Are("tall", "e/w", "canyon");
            Description = "You are in a tall E/W canyon. A low tight crawl goes 3 feet north and seems to open up.";

            EastTo<NSCanyon>();
            WestTo<DeadEnd8>();
            NorthTo<SwissCheeseRoom>();
        }
    }
}

