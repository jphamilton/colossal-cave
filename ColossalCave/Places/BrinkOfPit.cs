using ColossalCave.Objects;

namespace ColossalCave.Places
{
    public class BrinkOfPit : BelowGround
    {
        public override void Initialize()
        {
            Name = "At Brink of Pit";
            Description = 
                "You are on the brink of a thirty foot pit with a massive orange column down one wall. " +
                "You could climb down here but you could not get back up. " +
                "The maze continues at this level.";

            Has<MassiveOrangeColumn>();
            Has<Pit>();

            DownTo<BirdChamber>();
            WestTo<AlikeMaze10>();
            SouthTo<DeadEnd6>();
            NorthTo<AlikeMaze12>();
            EastTo<AlikeMaze13>();
        }
    }
}

