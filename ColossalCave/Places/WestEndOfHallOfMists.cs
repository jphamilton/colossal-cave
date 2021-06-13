using Adventure.Net;

namespace ColossalCave.Places
{
    public class WestEndOfHallOfMists : BelowGround
    {
        public override void Initialize()
        {
            Name = "At West End of Hall of Mists";
            Synonyms.Are("west", "w", "end", "of", "hall", "mists");
            Description = "You are at the west end of the hall of mists. A low wide crawl continues west and another goes north. To the south is a little passage 6 feet off the floor.";

            SouthTo<AlikeMaze1>();
            UpTo<AlikeMaze1>();
            EastTo<WestSideOfFissure>();
            WestTo<EastEndOfLongHall>();
            
            NorthTo(() =>
            {
                Output.Print("You have crawled through a very low wide passage parallel to and north of the hall of mists.\r\n");
                return Rooms.Get<WestSideOfFissure>();
            });
        }
    }
}

