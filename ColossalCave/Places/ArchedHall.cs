using Adventure.Net;

namespace ColossalCave.Places;

public class ArchedHall : BelowGround
{
    public override void Initialize()
    {
        Name = "Arched Hall";
        Synonyms.Are("arched, hall");
        Description =
            "You are in an arched hall. " +
            "A coral passage once continued up and east from here, but is now blocked by debris. " +
            "The air smells of sea water.";

        DownTo<ShellRoom>();
    }
}