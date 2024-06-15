using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class CrossOver : BelowGround
{
    public override void Initialize()
    {
        Name = "N/S and E/W Crossover";
        Synonyms.Are("n/s", "e/w", "crossover");

        WestTo<EastEndOfLongHall>();
        NorthTo<DeadEnd7>();
        EastTo<WestSideChamber>();
        SouthTo<WestEndOfLongHall>();
    }
}

public class Crossover : Scenic
{
    public override void Initialize()
    {
        Name = "crossover";
        Synonyms.Are("crossover", "cross");
        Description = "You know as much as I do at this point.";

        FoundIn<CrossOver>();
    }
}

