using ColossalCave.Places;
using Adventure.Net;
using ColossalCave.Objects;

namespace ColossalCave
{
    public class ColossalCaveStory : StoryBase
    {
        public ColossalCaveStory()
        {
            Story = "ADVENTURE";
        }

        protected override void OnInitialize()
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

            //Location = Rooms.Get<EndOfRoad>();
            Inventory.Add(Item.Get<SetOfKeys>());
            Location = Rooms.Get<OutsideGrate>();

        }

    
    }
}

