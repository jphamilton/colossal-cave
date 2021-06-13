﻿using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.Objects
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

            Before<Drop>(() =>
            {
                if (CurrentRoom.Location.Contains<VelvetPillow>())
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
            var shards = Adventure.Net.Objects.Get<Shards>();
            CurrentRoom.Objects.Add(shards);

        }
    }
}

