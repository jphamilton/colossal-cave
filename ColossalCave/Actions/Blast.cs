using Adventure.Net;
using ColossalCave.Places;

namespace ColossalCave.Actions;

public class Blast : Verb
{
    public Blast()
    {
        Name = "blast";
    }

    public bool Expects(Object noun, Preposition.With with, [Held] Object second)
    {
        if (second is not BlackMarkRod)
        {
            return Print("Blasting requires dynamite.");
        }

        return Print("Been eating those funny brownies again?");
    }

    public bool Expects()
    {
        var location = CurrentRoom.Location;
        var blackMarkRod = Objects.Get<BlackMarkRod>();

        if (location is not NeEnd and not SwEnd)
        {
            return Print("Frustrating, isn't it?");
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
            GameOver.Finished();
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
            GameOver.Dead();
            return true;
        }

        Print("There is a loud explosion, and you are suddenly splashed across the walls of the room.");
        GameOver.Dead();

        return true;
    }

}
