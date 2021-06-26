using ColossalCave.Things;
using ColossalCave.Places;
using Adventure.Net;
using ColossalCave.Actions;

namespace ColossalCave
{
    public class ColossalCaveStory : StoryBase
    {
        public ColossalCaveStory()
        {
            Story = "ADVENTURE";
            CurrentScore = 0;
            PossibleScore = 350;
            Flags.Add("dead", false);
        }

        protected override void Start()
        {
            Output.Bold("ADVENTURE");

            Output.Print(
                "By Will Crowther (1976) and Don Woods (1977)\n" +
                "Reconstructed in three steps by:\n" +
                "Donald Ekman, David M. Baggett (1993) and Graham Nelson (1994)\n" +
                "Ported from Inform 6 to C# (2009) by J.P. Hamilton\n" +
                "[In memoriam Stephen Bishop (1820?-1857): GN]\n"
            );

            // just for showing up!
            Score.Add(36);

            /*
               StartDaemon(dwarf);
               StartDaemon(pirate);
               StartDaemon(cave_closer);
             */

            Output.PrintLine();

            var lamp = Objects.Get<BrassLantern>();
            lamp.On = true;
            lamp.Light = true;
            lamp.PowerRemaining = 32;
            Inventory.Add(lamp);

            var coins = Objects.Get<RareCoins>();
            Inventory.Add(coins);

            Location = Rooms.Get<DeadEnd14>();

            //Location = Rooms.Get<EndOfRoad>();
        }

        public override void AfterTurn()
        {
            if (Flags["dead"])
            {
                GameOver.Dead();
                return;
            }


        }
    }
}

