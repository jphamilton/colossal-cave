using Adventure.Net;
using Adventure.Net.ActionRoutines;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class MingVase : Treasure
{
    public override void Initialize()
    {
        Name = "ming vase";
        Synonyms.Are("vase", "ming", "delicate");
        Description = "It's a delicate, precious, ming vase!";

        DepositPoints = 14;

        FoundIn<OrientalRoom>();

        Before<Attack>(() =>
        {
            RepaceVaseWithShards();
            Print("You have taken the vase and hurled it delicately to the ground.");
            return true;
        });

        Receive((obj) =>
        {
            Print("The vase is too fragile to use as a container.");
            return true;
        });

        Before<Drop>(() =>
        {
            if (CurrentRoom.Has<VelvetPillow>())
            {
                Print("(coming to rest, delicately, on the velvet pillow)");
                return false;
            }

            RepaceVaseWithShards();

            Print("The ming vase drops with a delicate crash.");

            return true;
        });
    }

    private void RepaceVaseWithShards()
    {
        Remove();

        var shards = Objects.Get<Shards>();

        shards.MoveToLocation();
    }
}

