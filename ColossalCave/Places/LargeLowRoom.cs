namespace ColossalCave.Places;

public class LargeLowRoom : BelowGround
{
    public override void Initialize()
    {
        Name = "Large Low Room";
        Synonyms.Are("large", "low", "room");
        Description = "You are in a large low room. Crawls lead north, se, and sw.";

        SouthWestTo<SlopingCorridor>();
        SouthEastTo<OrientalRoom>();
        NorthTo<DeadEndCrawl>();
    }
}
