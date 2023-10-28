namespace ColossalCave.Places;

public class EastEndOfLongHall : BelowGround
{
    public override void Initialize()
    {
        Name = "At East End of Long Hall";
        Synonyms.Are("long", "hall");
        Description =
        "You are at the east end of a very long hall apparently without side chambers. " +
         "To the east a low wide crawl slants up. " +
         "To the north a round two foot hole slants down.";

        EastTo<WestEndOfHallOfMists>();
        UpTo<WestEndOfHallOfMists>();
        WestTo<WestEndOfLongHall>();
        NorthTo<CrossOver>();
        DownTo<CrossOver>();
    }
}

