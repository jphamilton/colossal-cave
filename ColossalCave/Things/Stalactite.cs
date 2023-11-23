using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Stalactite : Scenic
{
    public override void Initialize()
    {
        Name = "stalactite";
        Synonyms.Are("stalactite", "stalagmite", "stalagtite", "large");
        Description = "You could probably climb down it, but you can forget coming back up.";

        FoundIn<AtopStalactite>();

        Before<LookUnder>(() => GetAGrip());
        Before<Push>(() => GetAGrip());
        Before<Take>(() => GetAGrip());
    }

    private bool GetAGrip()
    {
        return Print("Do get a grip on yourself.");
    }
}
