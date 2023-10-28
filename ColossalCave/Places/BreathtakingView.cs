using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class BreathtakingView : BelowGround
{
    private const string Ridiculous = "Don't be ridiculous!";

    public override void Initialize()
    {
        Name = "At Breath-Taking View";
        Synonyms.Are("breath-taking", "breathtaking", "breath", "taking", "view");
        Description =
            "You are on the edge of a breath-taking view. " +
            "Far below you is an active volcano, from which great gouts of molten lava come surging  out, " +
            "cascading back down into the depths. " +
            "The glowing rock fills the farthest reaches of the cavern with a blood-red glare, " +
            "giving everything an eerie, macabre appearance. " +
            "The air is filled with flickering sparks of ash and a heavy smell of brimstone. " +
            "The walls are hot to the touch, " +
            "and the thundering of the volcano drowns out all other sounds. " +
            "Embedded in the jagged roof far overhead " +
            "are myriad twisted formations composed of pure white alabaster, " +
            "which scatter the murky light into sinister apparitions upon the walls. " +
            "To one side is a deep gorge, filled with a bizarre chaos of tortured rock " +
            "which seems to have been crafted by the devil himself. " +
            "An immense river of fire crashes out from the depths of the volcano, " +
            "burns its way through the gorge, and plummets into a bottomless pit far off to your left. " +
            "To the right, an immense geyser of blistering steam erupts continuously " +
            "from a barren island in the center of a sulfurous lake, which bubbles ominously. " +
            "The far right wall is aflame with an incandescence of its own, " +
            "which lends an additional infernal splendor to the already hellish scene. " +
            "A dark, forboding passage exits to the south.";
        Light = true;

        SouthTo<JunctionWithWarmWalls>();
        OutTo<JunctionWithWarmWalls>();

        DownTo(() =>
        {
            Print(Ridiculous);
            return this;
        });

        Before<Jump>(() => Ridiculous);
    }
}

public class ActiveVolcano : Scenic
{
    public override void Initialize()
    {
        Name = "active volcano";
        Synonyms.Are("volcano", "rock", "active", "glowing", "blood", "blood-red", "red", "eerie", "macabre");
        Description =
            "Great gouts of molten lava come surging out of the volcano " +
            "and go cascading back down into the depths. " +
            "The glowing rock fills the farthest reaches of the cavern with a blood-red glare, " +
            "giving everything an eerie, macabre appearance.";

        FoundIn<BreathtakingView>();
    }
}

public class SparksOfAsh : Scenic
{
    public override void Initialize()
    {
        Name = "sparks of ash";
        Synonyms.Are("spark", "sparks", "ash", "air", "flickering");
        Description = "The sparks too far away for you to get a good look at them.";
        Attribute("multitude");

        FoundIn<BreathtakingView>();
    }
}

public class JaggedRoof : Scenic
{
    public override void Initialize()
    {
        Name = "jagged roof";
        Synonyms.Are("roof", "formations", "light", "apparaitions", "jagged", "twsited", "murky", "sinister");
        Description =
            "Embedded in the jagged roof far overhead are myriad twisted formations " +
             "composed of pure white alabaster, " +
             "which scatter the murky light into sinister apparitions upon the walls.";

        FoundIn<BreathtakingView>();
    }
}

public class DeepGorge : Scenic
{
    public override void Initialize()
    {
        Name = "deep gorge";
        Synonyms.Are("gorge", "chaos", "rock", "deep", "bizarre", "tortured");
        Description =
            "The gorge is filled with a bizarre chaos of tortured rock " +
            "which seems to have been crafted by the devil himself.";

        FoundIn<BreathtakingView>();
    }
}

public class RiverOfFire : Scenic
{
    public override void Initialize()
    {
        Name = "river of fire";
        Synonyms.Are("river", "fire", "depth", "pit", "fire", "fiery", "bottomless");
        Description =
            "The river of fire crashes out from the depths of the volcano, " +
            "burns its way through the gorge, and plummets into a bottomless pit far off to your left.";

        FoundIn<BreathtakingView>();
    }
}

public class ImmenseGeyser : Scenic
{
    public override void Initialize()
    {
        Name = "immense geyser";
        Synonyms.Are("geyser", "steam", "island", "lake", "immense", "blistering", "barren", "sulfrous", "sulferous", "sulpherous", "sulphrous", "bubbling");
        Description =
            "The geyser of blistering steam erupts continuously from a barren island " +
            "in the center of a sulfurous lake, which bubbles ominously.";

        FoundIn<BreathtakingView>();
    }
}

