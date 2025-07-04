using Adventure.Net;
using Adventure.Net.Things;
using ColossalCave.Actions;
using ColossalCave.Places;
using ColossalCave.Things;

namespace ColossalCave;

public class ColossalCaveStory : Story
{
    public ColossalCaveStory()
    {
        Name = "ADVENTURE";
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

        CurrentScore = 0;
        PossibleScore = 350;

        // just for showing up!
        Score.Add(36);

        var dwarf = Objects.Get<Dwarf>();
        dwarf.DaemonRunning = true;

        var pirate = Objects.Get<Pirate>();
        pirate.DaemonRunning = true;

        var closer = Objects.Get<CaveCloser>();
        closer.DaemonRunning = true;

        Output.PrintLine();

        Player.Location = Rooms.Get<EndOfRoad>();
    }

}