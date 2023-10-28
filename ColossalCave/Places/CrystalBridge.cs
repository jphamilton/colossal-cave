using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Places;

public class CrystalBridge : Door
{
    public override void Initialize()
    {
        Name = "crystal bridge";
        Synonyms.Are("crystal", "bridge");
        Description = "It spans the fissure, thereby providing you a way across.";
        InitialDescription = "A crystal bridge now spans the fissure.";
        Open = true;
        Absent = true;

        FoundIn<WestSideOfFissure, EastBankOfFissure>();

        Describe = () => "A crystal bridge now spans the fissure.";

        DoorDirection(() => In<WestSideOfFissure>() ? Direction<East>() : Direction<West>());

        DoorTo(() => In<WestSideOfFissure>() ? Room<EastBankOfFissure>() : Room<WestSideOfFissure>());
    }
}
