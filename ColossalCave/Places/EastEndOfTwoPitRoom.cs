using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class EastEndOfTwoPitRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "At East End of Twopit Room";
            Synonyms.Are("east", "end", "of", "twopit", "room");
            Description =
                "You are at the east end of the twopit room. " +
                "The floor here is littered with thin rock slabs, which make it easy to descend the pits. " +
                "There is a path here bypassing the pits to connect passages from east and west. " +
                "There are holes all over, " +
                "but the only big one is on the wall directly over the west pit where you can't get to it.";

            EastTo<SwissCheeseRoom>();
            WestTo<WestEndOfTwoPitRoom>();
            DownTo<EastPit>();
        }
    }
}

