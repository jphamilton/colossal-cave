using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;

namespace ColossalCave.Places;

public class CrystalBridge : Door
{
    public CrystalBridge()
    {
        Absent = true;
        Open = true;
    }

    public override void Initialize()
    {
        Name = "crystal bridge";
        Synonyms.Are("crystal", "bridge");
        Description = "It spans the fissure, thereby providing you a way across.";
        InitialDescription = "A crystal bridge now spans the fissure.";

        FoundIn<WestSideOfFissure, EastBankOfFissure>();

        Describe = () => "A crystal bridge now spans the fissure.";

        DoorDirection(() => Player.Location is WestSideOfFissure ? Direction<East>() : Direction<West>());

        DoorTo(() => Player.Location is WestSideOfFissure ? Room<EastBankOfFissure>() : Room<WestSideOfFissure>());
    }
}
