using ColossalCave.Places;

namespace ColossalCave.Things;

public class Boulders : Scenic
{
    public override void Initialize()
    {
        Name = "boulders";
        Synonyms.Are("boulder", "boulders", "rocks", "stones");
        Description = "They're just ordinary boulders.";
        // has multitude

        FoundIn<SlabRoom>();
    }
}

