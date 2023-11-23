using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class WindowOnPit1 : BelowGround
{
    public override void Initialize()
    {
        Name = "At Window on Pit";
        Synonyms.Are("window", "on", "pit", "east", "e");
        Description =
            "You're at a low window overlooking a huge pit, which extends up out of sight. " +
            "A floor is indistinctly visible over 50 feet below. " +
            "Traces of white mist cover the floor of the pit, becoming thicker to the right. " +
            "Marks in the dust around the window would seem to indicate that someone has been here recently. " +
            "Directly across the pit from you and 25 feet away " +
            "there is a similar window looking into a lighted room. " +
            "A shadowy figure can be seen there peering back at you.";

        CantGo = "The only passage is back east to Y2.";

        EastTo<Y2>();

        Before<WaveHands>(() =>
        {
            Print("The shadowy figure waves back at you!");
            return true;
        });

    }
}

public class Window : Scenic
{
    public override void Initialize()
    {
        Name = "window";
        Synonyms.Are("window", "low");
        Description = "It looks like a regular window.";
        Openable = true;

        FoundIn<WindowOnPit1, WindowOnPit2>();
    }
}

public class HugePit : Scenic
{
    public override void Initialize()
    {
        Name = "huge pit";
        Synonyms.Are("pit", "deep", "large");
        Description = "It's so deep you can barely make out the floor below, and the top isn't visible at all.";

        FoundIn<WindowOnPit1, WindowOnPit2>();
    }
}

public class MarksInTheDust : Scenic
{
    public override void Initialize()
    {
        Name = "marks in the dust";
        Description = "Evidently you're not alone here.";
        Multitude = true;

        FoundIn<WindowOnPit1, WindowOnPit2>();
    }
}

public class ShadowyFigure : Scenic
{
    public override void Initialize()
    {
        Name = "shadowy figure";
        Synonyms.Are("figure", "shadow", "person", "individual", "shadowy", "mysterious");
        Description = "The shadowy figure seems to be trying to attract your attention.";

        FoundIn<WindowOnPit1, WindowOnPit2>();
    }
}
