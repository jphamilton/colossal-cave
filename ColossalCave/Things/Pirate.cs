using Adventure.Net;
using Adventure.Net.Places;
using Adventure.Net.Things;
using Adventure.Net.Utilities;
using ColossalCave.Actions;
using ColossalCave.Places;
using System.Linq;

namespace ColossalCave.Things;

public class Pirate : Object
{
    public bool HasStolenSomething { get; set; }
    public bool HasBeenSpotted { get; set; }

    public override void Initialize()
    {
        Name = "";
        Synonyms.Are("");
        Description = "";

        Daemon = () =>
        {
            var bootyNearBy = false;
            var location = Player.Location;

            var noDwarf = ((BelowGround)Player.Location).NoDwarf;

            // very bizarre random check, but that's what was in the Inform 6 source
            if (Random.Number(1, 100) > 2 || location is Darkness || location is SecretCanyon || location.Light || noDwarf)
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
                if (HasBeenSpotted)
                {
                    return;
                }

                HasBeenSpotted = true;

                if (HasStolenSomething)
                {
                    DaemonRunning = false;
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

            if (HasStolenSomething)
            {
                return;
            }

            HasStolenSomething = true;

            if (HasBeenSpotted)
            {
                DaemonRunning = false;
            }

            var score = 0;

            foreach (var treasure in treasures)
            {
                if (Inventory.Contains(treasure))
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
    
}
