using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public enum PlantSize
{
    Tiny,
    Tall,
    Huge
}

public class Plant : Object
{
    public PlantSize Height { get; set; }

    public Plant()
    {
        Height = PlantSize.Tiny;
    }

    public override void Initialize()
    {
        Name = "plant";
        Synonyms.Are("plant", "beanstalk", "stalk", "bean", "giant", "tiny", "little", "murmuring", "twelve", "foot", "tall", "bellowing");

        FoundIn<WestPit>();

        Describe = () =>
        {
            if (Height == PlantSize.Tiny)
            {
                return "There is a tiny little plant in the pit, murmuring \"Water, water, ...\"";
            }
            else if (Height == PlantSize.Tall)
            {
                return "There is a 12-foot-tall beanstalk stretching up out of the pit, bellowing \"Water!! Water!!\"";
            }

            return "There is a gigantic beanstalk stretching all the way up to the hole.";
        };

        Before<Climb>(() =>
        {

            if (Height == PlantSize.Tiny)
            {
                return Print("It's just a little plant!");
            }
            else if (Height == PlantSize.Tall)
            {
                Print("You have climbed up the plant and out of the pit.\n");
                MovePlayer.To<WestEndOfTwoPitRoom>();
                return true;
            }

            Print("You clamber up the plant and scurry through the hole at the top.\n");

            MovePlayer.To<NarrowCorridor>();

            return true;
        });

        Before<Take>(() =>
        {
            Print("The plant has exceptionally deep roots and cannot be pulled free.");
            return true;
        });

        Before<Water>(Water);

        Before<Oil>(Water);

        //Before Examine:
        //self.describe();
        //rtrue;
    }

    private bool Water()
    {
        var bottle = Get<Bottle>();

        if (bottle.IsEmpty)
        {
            Print("You have nothing to water the plant with.");
            return true;
        }

        if (bottle.Contains<OilInTheBottle>())
        {
            Remove<OilInTheBottle>();
            Print("The plant indignantly shakes the oil off its leaves and asks, \"Water?\"");
            return true;
        }

        Remove<WaterInTheBottle>();

        var plantStickingUp = Get<PlantStickingUp>();

        switch (Height++)
        {
            case PlantSize.Tiny:
                Print("The plant spurts into furious growth for a few seconds.\n\n");
                plantStickingUp.Absent = false;
                break;

            case PlantSize.Tall:
                Print("The plant grows explosively, almost filling the bottom of the pit.\n\n");
                break;

            case PlantSize.Huge:
                Print("You've over-watered the plant! It's shriveling up! It's, it's...\n\n");
                plantStickingUp.Absent = true;
                Height = PlantSize.Tiny;
                break;
        }

        Describe();

        return true;
    }
}
