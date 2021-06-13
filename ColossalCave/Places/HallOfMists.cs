using Adventure.Net;
using ColossalCave.Objects;
using ColossalCave.Verbs;

namespace ColossalCave.Places
{
    public class HallOfMists : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Hall of Mists";
            Synonyms.Are("hall", "of", "mists");
            Description =
                "You are at one end of a vast hall stretching forward out of sight to the west. " +
                "There are openings to either side. " +
                "Nearby, a wide stone staircase leads downward. " +
                "The hall is filled with wisps of white mist swaying to and fro almost as if alive. " +
                "A cold wind blows up the staircase. " +
                "There is a passage at the top of a dome behind you. " +
                "\r\n\r\n" +
                "Rough stone steps lead up the dome.";

            Initial = () =>
            {
                if (!Visited)
                {
                    Score.Add(25, true);
                }
            };

            SouthTo<NuggetOfGoldRoom>();

            WestTo<EastBankOfFissure>();

            DownTo<HallOfMtKing>();

            UpTo(() =>
            {
                if (Inventory.Contains<LargeGoldNugget>())
                {
                    Output.Print("The dome is unclimbable.");
                    return this;
                }

                return Rooms.Get<TopOfSmallPit>();
            });

        }


    }
}

