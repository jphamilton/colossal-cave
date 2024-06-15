using Adventure.Net;
using Adventure.Net.ActionRoutines;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class NeEnd : BelowGround
{
    public override void Initialize()
    {
        Name = "NE End of Repository";
        Synonyms.Are("northeast", "ne", "end", "of", "repository");
        Description =
            "You are at the northeast end of an immense room, even larger than the giant room. " +
            "It appears to be a repository for the \"Adventure\" program. " +
            "Massive torches far overhead bathe the room with smoky yellow light. " +
            "Scattered about you can be seen a pile of bottles (all of them empty), " +
            "a nursery of young beanstalks murmuring quietly, a bed of oysters, " +
            "a bundle of black rods with rusty stars on their ends, and a collection of brass lanterns. " +
            "Off to one side a great many dwarves are sleeping on the floor, snoring loudly. " +
            "A sign nearby reads: \"Do not disturb the dwarves!\"";

        Light = true;

        SouthWestTo<SwEnd>();
    }
}

public class EnormousMirror : Object
{
    public override void Initialize()
    {
        Name = "enormous mirror";
        Synonyms.Are("mirror", "enormous", "huge", "big", "large", "suspended", "hanging", "vanity", "dwarvish");
        Description = "It looks like an ordinary, albeit enormous, mirror.";
        InitialDescription =
            "An immense mirror is hanging against one wall, and stretches to the other end of the room, " +
            "where various other sundry objects can be glimpsed dimly in the distance.";
        Static = true;

        FoundIn<NeEnd, SwEnd>();

        Before<Attack>(() =>
        {
            Print("You strike the mirror a resounding blow, whereupon it shatters into a myriad tiny fragments.\n\n");
            return Get<SleepingDwarves>().WakeUp();
        });
    }
}

public class AdventureGameMaterials : Scenic
{
    public override void Initialize()
    {
        Name = "collection of adventure game materials";
        Synonyms.Are("stuff", "junk", "materials", "torches", "objects", "adventure", "repository", "massive", "sundry");
        Description = "You've seen everything in here already, albeit in somewhat different contexts.";

        FoundIn<NeEnd, SwEnd>();

        Before<Take>(() => "Realizing that by removing the loot here you'd be ruining the game for future players, you leave the \"Adventure\" materials where they are.");


    }
}

public class SleepingDwarves : Scenic
{
    public override void Initialize()
    {
        Name = "sleeping dwarves";
        Synonyms.Are("dwarf", "dwarves", "sleeping", "snoring", "dozing", "snoozing");
        Description = "I wouldn't bother the dwarves if I were you.";
        IndefiniteArticle = "hundreds of angry";
        Animate = true;
        Multitude = true;

        FoundIn<NeEnd>();

        Before<Take>(() => "What, all of them?");

        Before<WakeOther>(() =>
        {
            Print("You prod the nearest dwarf, who wakes up grumpily, takes one look at you, curses, and grabs for his axe.\n\n");
            return WakeUp();
        });

        Before<Attack>(() => WakeUp());
    }

    public bool WakeUp()
    {
        Print("The resulting ruckus has awakened the dwarves. " +
            "There are now dozens of threatening little dwarves in the room with you! " +
            "Most of them throw knives at you! All of them get you!");
        Dead();
        return true;
    }
}
