using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Places
{
    public class WindowOnPit2 : BelowGround
    {
        public override void Initialize()
        {
            Name = "At Window on Pit";
            Synonyms.Are("window", "on", "pit");
            Description =
                "You're at a low window overlooking a huge pit, which extends up out of sight. " +
                "A floor is indistinctly visible over 50 feet below. " +
                "Traces of white mist cover the floor of the pit, becoming thicker to the left. " +
                "Marks in the dust around the window would seem to indicate that someone has been here recently. " +
                "Directly across the pit from you and 25 feet away " +
                "there is a similar window looking into a lighted room. " +
                "A shadowy figure can be seen there peering back at you.";

            CantGo = "The only passage is back west to the junction.";

            WestTo<JunctionOfThree>();

            Before<Jump>(() =>
            {
                Print("You jump and break your neck!");
                AfterLife.GoTo();
                return true;
            });

            Before<WaveHands>(() =>
            {
                Print("The shadowy figure waves back at you!");
                return true;
            });
        }
    }
}
