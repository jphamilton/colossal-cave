using Adventure.Net;
using Adventure.Net.Actions;
using Adventure.Net.Things;
using ColossalCave.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

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
            "\n\n" +
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
            if (Player.IsCarrying<LargeGoldNugget>())
            {
                Output.Print("The dome is unclimbable.");
                return this;
            }

            return Rooms.Get<TopOfSmallPit>();
        });

    }


}

#region Scenery

public class WideStoneStaircase : Scenic
{
    public override void Initialize()
    {
        Name = "wide stone staircase";
        Synonyms.Are("stair", "stairs", "staircase", "wide", "stone");
        Description = "The staircase leads down.";
        FoundIn<HallOfMists>();
    }
}

public class RoughStoneSteps : Scenic
{
    public override void Initialize()
    {
        Name = "rough stone steps";
        Synonyms.Are("stair", "stairs", "staircase", "rough", "stone");
        Description = "The rough stone steps lead up the dome.";
        Multitude = true;

        FoundIn<HallOfMists>();
    }
}

public class Dome : Scenic
{
    public override void Initialize()
    {
        Name = "dome";
        Synonyms.Are("dome");

        Before<Examine>(() =>
        {
            if (Player.IsCarrying<LargeGoldNugget>())
            {
                Print("I'm not sure you'll be able to get up it with what you're carrying.");
            }
            else
            {
                Print("It looks like you might be able to climb up it.");
            }

            return true;
        });

        Before<Climb>(() =>
        {
            Player.Location.DOWN();
            return true;
        });

    }
}

#endregion

