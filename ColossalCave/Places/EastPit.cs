using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class EastPit : BelowGround
    {
        public override void Initialize()
        {
            Name = "In East Pit";
            Synonyms.Are("in", "east", "pit");
            Description = "You are at the bottom of the eastern pit in the twopit room. There is a small pool of oil in one corner of the pit.";
            NoDwarf = true;

            UpTo<EastEndOfTwoPitRoom>();
        }
    }
}

