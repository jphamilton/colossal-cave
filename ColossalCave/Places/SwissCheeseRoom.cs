using Adventure.Net;
using Adventure.Net.Actions;
using Adventure.Net.Utilities;

namespace ColossalCave.Places
{
    public class SwissCheeseRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Swiss Cheese Room";
            Synonyms.Are("swiss", "cheese", "room");
            Description =
                "You are in a room whose walls resemble swiss cheese. " +
                "Obvious passages go west, east, ne, and nw. " +
                "Part of the room is occupied by a large bedrock block.";

            WestTo<EastEndOfTwoPitRoom>();
            SouthTo<TallEWCanyon>();
            NorthEastTo<Bedquilt>();
            //NorthWestTo<OrientalRoom>();
            EastTo<SoftRoom>();

            Before<Go>((Direction direction) =>
            {
                if ((direction is South && Random.Number(1, 100) <= 80) ||
                (direction is Northwest && Random.Number(1, 100) <= 50))
                {
                    Print("You have crawled around in some little holes and wound up back in the main passage.");
                    return true;
                }

                return false;
            });
        }
    }
}

