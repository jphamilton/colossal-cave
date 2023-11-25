using Adventure.Net;
using ColossalCave.Actions;
using ColossalCave.Places;
using ColossalCave.Things;

namespace ColossalCave;

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
            "Ported from Inform 6 to C# by J.P. Hamilton (2009)\n" +
            "[In memoriam Stephen Bishop (1820?-1857): GN]\n"
        );

        // just for showing up!
        Score.Add(36);

        var dwarf = Objects.Get<Dwarf>();
        dwarf.DaemonRunning = true;

        var pirate = Objects.Get<Pirate>();
        pirate.DaemonRunning = true;

        var closer = Objects.Get<CaveCloser>();
        closer.DaemonRunning = true;

        Output.PrintLine();

        Location = Rooms.Get<EndOfRoad>();
    }

}