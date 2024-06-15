using Adventure.Net;
using Adventure.Net.ActionRoutines;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class BedrockBlock : Scenic
{
    public override void Initialize()
    {
        Name = "bedrock block";
        Synonyms.Are("block", "bedrock", "large");
        Description = "";

        FoundIn<SwissCheeseRoom>();

        Before<LookUnder>(() => HaHa());
        Before<Push>(() => HaHa());
        Before<Pull>(() => HaHa());
        Before<Take>(() => HaHa());

    }

    private bool HaHa()
    {
        return Print("Surely you're joking.");
    }
}
