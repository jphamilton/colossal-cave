using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class WestPit : BelowGround
{
    public override void Initialize()
    {
        Name = "In West Pit";

        Synonyms.Are("in", "west", "pit");

        Description =
            "You are at the bottom of the western pit in the twopit room. " +
            "There is a large hole in the wall about 25 feet above you.";

        NoDwarf = true;

        Before<Climb>(() =>
        {
            if (Noun is Plant)
            {
                return false;
            }

            var plant = Objects.Get<Plant>();

            if (plant.Height == PlantSize.Tiny)
            {
                Print("There is nothing here to climb. Use \"up\" or \"out\" to leave the pit.");
                return true;
            }

            return false;
        });

        UpTo<WestEndOfTwoPitRoom>();
    }
}
