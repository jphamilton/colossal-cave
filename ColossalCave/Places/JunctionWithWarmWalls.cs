using Adventure.Net;

namespace ColossalCave.Places;

public class JunctionWithWarmWalls : BelowGround
{
    public override void Initialize()
    {
        Name = "At Junction With Warm Walls";
        Synonyms.Are("junction", "with", "warm", "walls");
        Description =
            "The walls are quite warm here. " +
             "From the north can be heard a steady roar, " +
             "so loud that the entire cave seems to be trembling. " +
             "Another passage leads south, and a low crawl goes east.";
        NoDwarf = true;

        SouthTo<ForkInPath>();
        NorthTo<BreathtakingView>();
        EastTo<ChamberOfBoulders>();
    }
}
