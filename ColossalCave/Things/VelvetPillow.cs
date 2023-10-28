using Adventure.Net;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class VelvetPillow : Object
{
    public override void Initialize()
    {
        Name = "velvet pillow";
        Synonyms.Are("pillow", "velvet", "small");
        Description = "It's just a small velvet pillow.";
        InitialDescription = "A small velvet pillow lies on the floor.";

        FoundIn<SoftRoom>();
    }
}
