using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class ChamberOfBoulders : BelowGround
{
    public override void Initialize()
    {
        Name = "In Chamber of Boulders";
        Synonyms.Are("chamber", "of", "boulders");
        Description =
            "You are in a small chamber filled with large boulders. " +
            "The walls are very warm, causing the air in the room to be almost stifling from the heat. " +
            "The only exit is a crawl heading west, through which is coming a low rumbling.";
        NoDwarf = true;

        WestTo<JunctionWithWarmWalls>();
        OutTo<JunctionWithWarmWalls>();
    }
}

public class Boulders : Scenic
{
    public override void Initialize()
    {
        Name = "boulders";
        Synonyms.Are("boulder", "boulders", "rocks", "stones");
        Description = "They're just ordinary boulders. They're warm.";
        Attribute("multitude");

        FoundIn<ChamberOfBoulders>();

        Before<LookUnder, Pull, Push>(() => Print("You'd have to blast them aside."));

    }
}

public class RareSpices : Treasure
{
    public override void Initialize()
    {
        Name = "rare spices";
        Synonyms.Are("spices", "spice", "rare", "exotic");
        IndefiniteArticle = "a selection of";
        DepositPoints = 14;
        Attribute("multitude");

        FoundIn<ChamberOfBoulders>();

        Before<Smell, Examine>(() => Print("They smell wonderfully exotic!"));
    }
}
