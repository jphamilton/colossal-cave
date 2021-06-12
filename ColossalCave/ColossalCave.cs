using ColossalCave.Places;
using Adventure.Net;

namespace ColossalCave
{
    public class ColossalCaveStory : StoryBase
    {
        public ColossalCaveStory()
        {
            Story = "ADVENTURE";
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

            Output.PrintLine();

            Location = Rooms.Get<EndOfRoad>();
        }

    
    }
}

