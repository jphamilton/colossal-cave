﻿using Adventure.Net;
using Adventure.Net.Utilities;
using ColossalCave.Actions;
using ColossalCave.Places;
using System.Linq;

namespace ColossalCave.Things
{
    public class Pirate : Object
    {
        private bool hasStolenSomething = false;
        private bool hasBeenSpotted = false;
        
        public override void Initialize()
        {
            Name = "";
            Synonyms.Are("");
            Description = "";

            Daemon = () =>
            {
                var bootyNearBy = false;
                var location = CurrentRoom.Location;

                if (Random.Number(1,100) > 2 || location is Darkness || location is SecretCanyon || location.Light || NoDwarf)
                {
                    return;
                }

                if (Get<Dwarf>().InRoom)
                {
                    Print("\nA bearded pirate appears, catches sight of the dwarf and runs away.");
                    return;
                }

                var treasures =
                    from t in Objects.All
                    where t is Treasure && t.InScope
                    select t;

                bootyNearBy = treasures.Any();

                if (!bootyNearBy)
                {
                    if (hasBeenSpotted)
                    {
                        return;
                    }

                    hasBeenSpotted = true;

                    if (hasStolenSomething)
                    {
                        DaemonStarted = false;
                    }

                    Print(
                        "\nThere are faint rustling noises from the darkness behind you. " +
                        "As you turn toward them, you spot a bearded pirate. " +
                        "He is carrying a large chest. " +
                        "\n\n " +
                        "\"Shiver me timbers!\" he cries, \"I've been spotted! " +
                        "I'd best hie meself off to the maze to hide me chest!\" " +
                        "\n\n " +
                        "With that, he vanishes into the gloom."
                    );

                    return;
                }

                // thar be booty!

                if (hasStolenSomething)
                {
                    return;
                }

                hasStolenSomething = true;

                if (hasBeenSpotted)
                {
                    DaemonStarted = false;
                }

                var score = 0;

                foreach(var treasure in treasures)
                {
                    if (treasure.InInventory)
                    {
                        score -= 5;
                    }

                    treasure.MoveTo<DeadEnd13>();
                }

                Print(
                    "\nOut from the shadows behind you pounces a bearded pirate! " +
                    "\"Har, har,\" he chortles. \"I'll just take all this booty and hide it away " +
                    "with me chest deep in the maze!\" " +
                    "He snatches your treasure and vanishes into the gloom."
                );

                if (score != 0)
                {
                    Score.Add(score, true);
                }

            };
        }

        private bool NoDwarf
        {
            get
            {
                return ((BelowGround)CurrentRoom.Location).NoDwarf;
            }
        }
    }
}
