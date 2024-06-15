using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using Adventure.Net.Utilities;
using ColossalCave.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class Y2 : BelowGround
{
    public override void Initialize()
    {
        Name = "At \"Y2\"";
        Description =
            "You are in a large room, with a passage to the south, " +
            "a passage to the west, and a wall of broken rock to the east. " +
            "There is a large \"Y2\" on a rock in the room's center.";


        SouthTo<LowNSPassage>();
        EastTo<JumbleOfRock>();
        WestTo<WindowOnPit1>();


        After<Look>(() =>
        {
            if (Random.Number(1, 100) < 25)
            {
                Print("\nA hollow voice says, \"Plugh.\"\n");
            }
        });

        Before<Plugh>(() =>
        {
            MovePlayer.To<InsideBuilding>();
            return true;
        });

        Before<Plover>(() =>
        {
            if (!Room<PloverRoom>().Visited)
            {
                return false;
            }

            if (Player.IsCarrying<EggSizedEmerald>())
            {
                Move<EggSizedEmerald>.To<PloverRoom>();
                Score.Add(-5);
            }

            MovePlayer.To<PloverRoom>();

            return true;
        });

    }
}

public class Y2Rock : Supporter
{
    public override void Initialize()
    {
        Name = "\"Y2\" rock";
        Synonyms.Are("rock", "y2");
        Description = "There is a large \"Y2\" painted on the rock.";

        FoundIn<Y2>();
    }
}