using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class SuspendedMirror : Object
{
    public override void Initialize()
    {
        Name = "suspended mirror";
        Synonyms.Are("mirror", "massive", "enormous", "hanging", "suspended", "two-sided", "two", "sided");
        Description = "The mirror is obviously provided for the use of the dwarves who, " +
            "as you know, are extremely vain.";
        InitialDescription = "The mirror is obviously provided for the use of the dwarves who, " +
            "as you know, are extremely vain.";
        Static = true;

        FoundIn<MirrorCanyon>();

        Before<Attack>(() => CantReach());
        Before<Remove>(() => CantReach());
    }

    private bool CantReach()
    {
        Print("You can't reach it from here.");
        return true;
    }
}
