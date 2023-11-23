using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class SlitInStreambed : AboveGround
{
    public override void Initialize()
    {
        Name = "At Slit In Streambed";
        Synonyms.Are("slit", "streambed");
        Description = "At your feet all the water of the stream splashes into a 2-inch slit in the rock. Downstream the streambed is bare rock.";

        NorthTo<Valley>();
        EastTo<Forest1>();
        WestTo<Forest1>();
        SouthTo<OutsideGrate>();

        Before<Down>(() => Print("You don't fit through a two-inch slit!"));

        Before<In>(() => Print("You don't fit through a two-inch slit!"));

    }
}

public class TwoInchSlit : Scenic
{
    public override void Initialize()
    {
        Name = "2-inch slit";
        Synonyms.Are("slit", "two", "inch", "2-inch");
        Description = "It's just a 2-inch slit in the rock, through which the stream is flowing.";

        FoundIn<SlitInStreambed>();

        Before<Enter>(() => Print("You don't fit through a two-inch slit!"));

    }
}

