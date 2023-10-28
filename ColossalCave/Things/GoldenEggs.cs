using Adventure.Net;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class GoldenEggs : Treasure
{
    public override void Initialize()
    {
        Name = "nest of golden eggs";
        Synonyms.Are("eggs", "egg", "nest", "golden", "beautiful");
        Description = "The nest is filled with beautiful golden eggs!";
        InitialDescription = "There is a large nest here, full of golden eggs!";
        DepositPoints = 14;

        FoundIn<GiantRoom>();
    }
}
