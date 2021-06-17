using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class ThinRockSlabs : Scenic
    {
        public override void Initialize()
        {
            Name = "thin rock slabs";
            Synonyms.Are("slabs", "slab", "rocks", "stairs", "thin", "rock");
            Description = "They almost form natural stairs down into the pit.";
            // has multitude

            FoundIn<EastEndOfTwoPitRoom>();

            Before<LookUnder>(DontCallMeShirley);
            Before<Push>(DontCallMeShirley);
            Before<Pull>(DontCallMeShirley);
            Before<Take>(DontCallMeShirley);
        }

        public bool DontCallMeShirley()
        {
            Print("Surely you're joking. You'd have to blast them aside.");
            return true;
        }
    }
}

