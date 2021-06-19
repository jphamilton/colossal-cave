using Adventure.Net;

namespace ColossalCave.Places
{
    public class WestSideOfFissure : FissureRoom
    {
        public override void Initialize()
        {
            base.Initialize();

            Name = "West Side of Fissure";
            Synonyms.Are("west", "w", "bank", "side", "of", "fissure");
            Description = "You are on the west side of the fissure in the hall of mists.";

            WestTo<WestEndOfHallOfMists>();
            
            EastTo(CannotCross);

            NorthTo(() =>
            {
                Output.Print("You have crawled through a very low wide passage parallel to and north of the hall of mists.\r\n");
                return Rooms.Get<WestEndOfHallOfMists>();
            });

        }

        public void BridgeAppears()
        {
            Get<CrystalBridge>().IsAbsent = false;
            EastTo<CrystalBridge>();
        }
        public void BridgeDisappears()
        {
            Get<CrystalBridge>().IsAbsent = true;
            EastTo(CannotCross);
        }

    }
}
