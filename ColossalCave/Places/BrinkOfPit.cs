using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class BrinkOfPit : BelowGround
{
    public override void Initialize()
    {
        Name = "At Brink of Pit";
        Description =
            "You are on the brink of a thirty foot pit with a massive orange column down one wall. " +
            "You could climb down here but you could not get back up. " +
            "The maze continues at this level.";

        DownTo<BirdChamber>();
        WestTo<AlikeMaze10>();
        SouthTo<DeadEnd6>();
        NorthTo<AlikeMaze12>();
        EastTo<AlikeMaze13>();
    }
}

public class MassiveOrangeColumn : Scenic
{
    public override void Initialize()
    {
        Name = "massive orange column";
        Synonyms.Are("column", "massive", "orange", "big", "huge");
        Description = "It looks like you could climb down it.";

        FoundIn<BrinkOfPit>();

        Before<Climb>(() =>
        {
            CurrentRoom.Location.DOWN();
            return true;
        });

    }
}

public class Pit : Scenic
{
    public override void Initialize()
    {
        Name = "pit";
        Synonyms.Are("pit", "thirty", "foot", "thirty-foot");
        Description = "You'll have to climb down to find out anything more...";

        FoundIn<BrinkOfPit>();

        Before<Climb>(() =>
        {
            CurrentRoom.Location.DOWN();
            return true;
        });
    }
}


