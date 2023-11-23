using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class SideOfChasm : BelowGround
{
    public SideOfChasm()
    {
        Before<Jump>(() =>
        {
            var rickety = Get<RicketyBridge>();

            if (CurrentRoom.Has<RicketyBridge>())
            {
                return Print("I respectfully suggest you go across the bridge instead of jumping.");
            }

            Print("You didn't make it.");
            GameOver.Dead();
            return true;
        });
    }

    protected Room CrossRicketyBridge()
    {
        var troll = Objects.Get<BurlyTroll>();
        var bear = Objects.Get<Bear>();
        
        if (troll.HasCaughtTreasure)
        {
            troll.HasCaughtTreasure = false;
            
            if (bear.IsFollowingYou)
            {
                var ricketyBridge = Rooms.Get<RicketyBridge>();
                var wreckedBridge = Objects.Get<WreckedBridge>();

                bear.Remove();
                bear.DaemonStarted = false;

                ricketyBridge.Absent = true;
                ricketyBridge.Remove();

                wreckedBridge.Absent = false;

                Output.Print("Just as you reach the other side, the bridge buckles beneath the weight of the bear, " +
                     "which was still following you around. " +
                     "You scrabble desperately for support, " +
                     "but as the bridge collapses you stumble back and fall into the chasm.");

                GameOver.Dead();
                return Location;
            }
            
            return Room<RicketyBridge>();
        }

        if (troll.InRoom)
        {
            Print("The troll refuses to let you cross.");
            return this;
        }

        if (!troll.ScaredOff)
        {
            troll.MoveToLocation();
            Print("The troll steps out from beneath the bridge and blocks your way.");
            return this;
        }

        return Room<RicketyBridge>();
    }

    public override void Initialize()
    {
        // no op   
    }
}

public class SwSideOfChasm : SideOfChasm
{
    public override void Initialize()
    {
        Name = "On SW Side of Chasm";
        Synonyms.Are("southwest", "sw", "side", "of", "chasm");
        Description =
            "You are on one side of a large, deep chasm. " +
            "A heavy white mist rising up from below obscures all view of the far side. " +
            "A southwest path leads away from the chasm into a winding corridor.";

        CantGo = "The path winds southwest.";

        NorthEastTo(CrossRicketyBridge);
        SouthWestTo<SlopingCorridor>();
        DownTo<SlopingCorridor>();
    }
}

public class NeSideOfChasm : SideOfChasm
{
    public override void Initialize()
    {
        Name = "On NE Side of Chasm";
        Synonyms.Are("northeast", "ne", "side", "of", "chasm");
        Description = "You are on the far side of the chasm. A northeast path leads away from the chasm on this side.";
        NoDwarf = true;

        SouthWestTo(CrossRicketyBridge);
        NorthEastTo<Corridor>();
    }
}

public class RicketyBridge : Door
{
    public override void Initialize()
    {
        Name = "rickety bridge";
        Synonyms.Are("bridge", "rickety", "unstable", "wobbly", "rope");
        Description = "It just looks like an ordinary, but unstable, bridge.";
        Open = true;

        FoundIn<SwSideOfChasm, NeSideOfChasm>();

        Describe = () =>
        {
            var result =
                "A rickety wooden bridge extends across the chasm, vanishing into the mist. " +
                "\n\nA sign posted on the bridge reads, \"Stop! Pay troll!\"\n";

            var troll = Get<BurlyTroll>();

            if (!troll.InRoom)
            {
                result += "The troll is nowhere to be seen.";
            }

            return result;
        };

        DoorDirection(() =>
        {
            if (CurrentRoom.Is<SwSideOfChasm>())
            {
                return Direction<Northeast>();
            }

            return Direction<Southwest>();
        });

        DoorTo(() =>
        {
            if (CurrentRoom.Is<SwSideOfChasm>())
            {
                return Rooms.Get<NeSideOfChasm>();
            }

            return Rooms.Get<SwSideOfChasm>();
        });
    }
}

public class WreckedBridge : Object
{
    public WreckedBridge()
    {
        Absent = true;
    }

    public override void Initialize()
    {
        Name = "wreckage of bridge";
        Synonyms.Are("wreckage", "wreck", "bridge", "dead", "bear");
        InitialDescription = "The wreckage of the troll bridge (and a dead bear) can be seen at the bottom of the chasm.";
        Static = true;

        FoundIn<SwSideOfChasm, NeSideOfChasm>();

        Before<Examine>(() => "The wreckage is too far below.");
    }
}

public class BurlyTroll : Object
{
    public bool HasCaughtTreasure { get; set; }
    public bool ScaredOff { get; set; }

    public override void Initialize()
    {
        Name = "burly troll";
        Synonyms.Are("burly", "troll");
        Description = "Trolls are close relatives with rocks and have skin as tough as that of a rhinoceros.";
        InitialDescription = "A burly troll stands by the bridge and insists you throw him a treasure before you may cross.";
        Animate = true;

        Before<Attack>(() => "The troll laughs aloud at your pitiful attempt to injure him.");

        Before<ThrowAt, Give>(() =>
        {
            if (Noun is Treasure)
            {
                Noun.Remove();
                MoveTo<RicketyBridge>();
                HasCaughtTreasure = true;
                Print("The troll catches your treasure and scurries away out of sight.");
                Score.Add(-5, true);
                return true;
            }

            if (Noun is TastyFood)
            {
                return Print("Gluttony is not one of the troll's vices. Avarice, however, is.");
            }

            return Print($"The troll deftly catches {Noun.DefiniteArticle} {Noun.Name}, examines it carefully, and tosses it back, declaring, " +
              "\"Good workmanship, but it's not valuable enough.\"");
        });

        Before<Order>(() => "You'll be lucky.");

        Before<Ask>(() => "Trolls make poor conversation.");
        
        Before<Answer>(() => "Trolls make poor conversation.");

    }
}
