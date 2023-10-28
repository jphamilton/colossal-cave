namespace ColossalCave.Places;

public class DirtyPassage : BelowGround
{
    public override void Initialize()
    {
        Name = "Dirty Passage";
        Synonyms.Are("dirty", "passage");
        Description =
            "You are in a dirty broken passage. " +
            "To the east is a crawl. " +
            "To the west is a large passage. " +
            "Above you is a hole to another passage.";

        EastTo<BrinkOfPit>();
        UpTo<LowNSPassage>();
        WestTo<DustyRockRoom>();
    }
}

