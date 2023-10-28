using ColossalCave.Things;

namespace ColossalCave.Places;

public class NuggetOfGoldRoom : BelowGround
{
    public override void Initialize()
    {
        Name = "Low Room";
        Description = "This is a low room with a crude note on the wall:\n\n\"You won't get it up the steps\".";

        NorthTo<HallOfMists>();
    }
}

public class CrudeNote : Scenic
{
    public override void Initialize()
    {
        Name = "note";
        Synonyms.Are("note", "crude");
        Description = "The note says, \"You won't get it up the steps\".";

        FoundIn<NuggetOfGoldRoom>();
    }
}

