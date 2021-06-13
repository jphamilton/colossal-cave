using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Places
{
    public class CrystalBridge : Door
    {
        public override void Initialize()
        {
            Name = "crystal bridge";
            Synonyms.Are("crystal", "bridge");
            Description = "It spans the fissure, thereby providing you a way across.";
            InitialDescription = "A crystal bridge now spans the fissure.";
            IsOpen = true;

            // found_in On_East_Bank_Of_Fissure West_Side_Of_Fissure,
            Describe = () =>
            {
                return "A crystal bridge now spans the fissure.";
            };

            DoorDirection = () =>
            {
                if (In<WestSideOfFissure>())
                    return Direction<East>();
                return Direction<West>();
            };

            DoorTo = () =>
            {
                if (In<WestSideOfFissure>())
                {
                    return Room<EastBankOfFissure>();
                }

                return Room<WestSideOfFissure>();
            };
        }
    }
}
