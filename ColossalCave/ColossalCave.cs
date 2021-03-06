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

            var dwarf = Objects.Get<Dwarf>();
            dwarf.DaemonStarted = true;

            var pirate = Objects.Get<Pirate>();
            pirate.DaemonStarted = true;

            var closer = Objects.Get<CaveCloser>();
            closer.DaemonStarted = true;

            Output.PrintLine();

            // --- Testing
            //var timer = Objects.Get<EndgameTimer>();
            //timer.TimeLeft = 2;

            //var lamp = Objects.Get<BrassLantern>();
            //lamp.On = true;
            //lamp.Light = true;
            //Inventory.Add(lamp);

            //Global.TreasuresFound = Global.MaxTreasures;
            //Location = Rooms.Get<ShellRoom>();
            // ---


            Location = Rooms.Get<EndOfRoad>();
        }

        
    }
}

