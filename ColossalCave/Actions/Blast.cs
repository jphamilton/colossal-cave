using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using ColossalCave.Places;

namespace ColossalCave.Actions;

public class BlastWith : Routine
{
    public BlastWith()
    {
        Verbs = ["blast"];
        Prepositions = ["with"];
        Requires = [O.Noun, O.Held];
    }

    public override bool Handler(Object first, Object held)
    {
        if (held is not BlackMarkRod)
        {
            return Fail("Blasting requires dynamite.");
        }

        return Fail("Been eating those funny brownies again?");
    }
}

public class Blast : Routine
{
    public Blast()
    {
        Verbs = ["blast"];
    }

    public override bool Handler(Object _, Object __)
    {
        var location = Player.Location;
        var blackMarkRod = Objects.Get<BlackMarkRod>();

        if (location is not NeEnd and not SwEnd)
        {
            return Fail("Frustrating, isn't it?");
        }

        if (location is SwEnd && blackMarkRod.Location is NeEnd)
        {
            Score.Add(35, true);
            var message =
                "There is a loud explosion, and a twenty-foot hole appears in the far wall, " +
                "burying the dwarves in the rubble. " +
                "You march through the hole and find yourself in the main office, " +
                "where a cheering band of friendly elves carry the conquering adventurer off into the sunset.";
            Print(message);
            GameOver.Win();
            return true;
        }

        if (location is NeEnd && blackMarkRod.Location is SwEnd)
        {
            Score.Add(20, true);
            var message =
                "There is a loud explosion, and a twenty-foot hole appears in the far wall, " +
                "burying the snakes in the rubble. " +
                "A river of molten lava pours in through the hole, destroying everything in its path, including you!";
            Print(message);
            Dead();
            return true;
        }

        Print("There is a loud explosion, and you are suddenly splashed across the walls of the room.");
        Dead();

        return true;
    }
}
