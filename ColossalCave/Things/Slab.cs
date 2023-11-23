using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Slab : Scenic
{
    public override void Initialize()
    {
        Name = "slab";
        Synonyms.Are("slab", "immense");
        Description = "It is now the floor here.";

        FoundIn<SlabRoom>();

        Before<LookUnder>(() => Joking());
        Before<Push>(() => Joking());
        Before<Pull>(() => Joking());
        Before<Take>(() => Joking());
    }

    private bool Joking()
    {
        return Print("Surely you're joking.");
    }
}
