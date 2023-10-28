using Adventure.Net;

namespace ColossalCave.Places;

public class Corridor : BelowGround
{
    public override void Initialize()
    {
        Name = "In Corridor";
        Synonyms.Are("corridor");
        Description = "You're in a long east/west corridor. A faint rumbling noise can be heard in the distance.";
        NoDwarf = true;

        WestTo<NeSideOfChasm>();
        EastTo<ForkInPath>();
    }
}
