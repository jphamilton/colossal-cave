using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.Places
{
    public class MingVase : Treasure
    {
        public override void Initialize()
        {
            Name = "ming vase";
            Synonyms.Are("vase", "ming", "delicate");
            Description = "It's a delicate, precious, ming vase!";
            
            DepositPoints = 14;

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

            After<Drop>(() =>
            {
                if (CurrentRoom.Location.Contains<VelvetPillow>())
                {
                    Print("(coming to rest, delicately, on the velvet pillow)");
                    return;
                }

                RepaceVaseWithShards();

                Print("The ming vase drops with a delicate crash.");
            });
        }

        private void RepaceVaseWithShards()
        {
            Remove();
            var shards = Adventure.Net.Objects.Get<Shards>();
            CurrentRoom.Objects.Add(shards);

        }
    }
}

//Treasure -> ming_vase "ming vase"
//  with  name 'vase' 'ming' 'delicate',
//        description "It's a delicate, precious, ming vase!",
//        after [;
//          Drop:
//            if (velvet_pillow in location) {
//                print "(coming to rest, delicately, on the velvet pillow)^";
//                rfalse;
//            }
//            remove ming_vase;
//            move shards to location;
//            "The ming vase drops with a delicate crash.";
//        ],
//        before [;
//          Attack:
//            remove ming_vase;
//            move shards to location;
//            "You have taken the vase and
//            hurled it delicately to the ground.";
//          Receive:
//            "The vase is too fragile to use as a container.";
//        ],
//        depositpoints 14;

