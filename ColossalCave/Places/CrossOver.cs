using Adventure.Net;

namespace ColossalCave.Places
{
    public class CrossOver : BelowGround
    {
        public override void Initialize()
        {
            Name = "N/S and E/W Crossover";
            Synonyms.Are("n/s", "and", "e/w", "crossover");

            Has<Things.CrossOver>();

            WestTo<EastEndOfLongHall>();
            NorthTo<DeadEnd7>();
            //EastTo<WestSideChamber>();
            SouthTo<WestEndOfLongHall>();
        }
    }
}

