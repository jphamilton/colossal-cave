using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Bear : Object
{
    public bool IsFollowingYou { get; set; } = false;
    public bool IsFriendly { get; set; } = false;

    public override void Initialize()
    {
        Name = "large cave bear";
        Synonyms.Are("bear", "large", "tame", "ferocious", "cave");
        Animate = true;

        FoundIn<BarrenRoom>();

        Describe = () =>
        {
            if (IsFollowingYou)
            {
                return "You are being followed by a very large, tame bear.";
            }

            if (!IsFriendly)
            {
                return "There is a ferocious cave bear eyeing you from the far end of the room!";
            }

            if (In<BarrenRoom>())
            {
                return "There is a gentle cave bear sitting placidly in one corner.";
            }

            return "There is a contented-looking bear wandering about nearby.";
        };

        Before<Attack>(() =>
        {
            if (IsCarrying<Axe>())
            {
                return ThrowAt(Noun);
            }

            if (IsFriendly)
            {
                return Print("The bear is confused; he only wants to be your friend.");
            }

            return Print("With what? Your bare hands? Against *his* bear hands??");
        });

        Before<ThrowAt>(() => ThrowAt(Noun));

        Before<Give>(() => Give(Noun));

        Before<Order, Ask, Answer>(() => Print("This is a Bear of very little brain."));

        Before<Examine>(() =>
        {
            const string large = "The bear is extremely large, ";

            if (IsFriendly)
            {
                return Print($"{large}, but appears to be friendly.");
            }

            return Print($"{large}, and seems quite ferocious!");
        });

        Before<Take, Catch>(() =>
        {
            var chain = Get<GoldenChain>();

            if (!IsFriendly)
            {
                return Print("Surely you're joking!");
            }

            if (chain.Locked)
            {
                return Print("The bear is still chained to the wall.");
            }

            IsFollowingYou = true;
            DaemonStarted = true;

            return Print("Ok, the bear's now following you around.");
        });

        Before<Drop, Release>(() =>
        {
            if (!IsFollowingYou)
            {
                return Print("What?");
            }

            IsFollowingYou = false;
            DaemonStarted = false;

            var troll = Get<BurlyTroll>();

            if (troll.InRoom)
            {
                troll.ScaredOff = true;
                troll.Remove();
                
                return Print("The bear lumbers toward the troll, who lets out a startled shriek and scurries away. " +
                    "The bear soon gives up the pursuit and wanders back.");
            }

            return Print("The bear wanders away from you.");
        });

        Daemon = () =>
        {
            var bear = Get<Bear>();

            if (CurrentRoom.Location is Darkness)
            {
                return;
            }

            if (bear.InRoom)
            {
                if (CurrentRoom.Is<BreathtakingView>())
                {
                    Print("\nThe bear roars with delight.");
                }

                return;
            }

            bear.MoveToLocation();

            Print("\nThe bear lumbers along behind you.");
        };
    }

    public bool ThrowAt(Object obj)
    {
        if (obj is not Axe)
        {
            return Give(obj);
        }

        if (IsFriendly)
        {
            Print("The bear is confused; he only wants to be your friend.");
        }

        var axe = (Axe)obj;
        axe.MoveToLocation();
        axe.IsNearBear = true;

        return Print("The axe misses and lands near the bear where you can't get at it.");
    }

    public bool Give(Object obj)
    {
        if (obj is TastyFood)
        {
            var axe = Get<Axe>();
            axe.IsNearBear = false;
            obj.Remove();
            IsFriendly = true;
            return Print("The bear eagerly wolfs down your food, after which he seems to calm down considerably and even becomes rather friendly.");
        }

        if (IsFriendly)
        {
            return Print("The bear doesn't seem very interested in your offer.");
        }

        return Print("Uh-oh -- your offer only makes the bear angrier!");
    }
}
