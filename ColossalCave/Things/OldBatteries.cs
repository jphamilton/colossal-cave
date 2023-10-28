using Adventure.Net;
using ColossalCave.Actions;

namespace ColossalCave.Things;

public class OldBatteries : Object
{
    public override void Initialize()
    {
        Name = "worn-out batteries";
        Synonyms.Are("batteries, battery, worn, out, worn-out");
        Description = "They look like ordinary batteries.";
        InitialDescription = "Some worn-out batteries have been discarded nearby.";

        Before<Count>(() => "A pair.");
    }
}
