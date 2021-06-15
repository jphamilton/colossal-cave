using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class Slab : Scenic
    {
        public override void Initialize()
        {
            Name = "slab";
            Synonyms.Are("slab", "immense");
            Description = "It is now the floor here.";

            Before<LookUnder>(() => Joking());
            Before<Push>(() => Joking());
            Before<Pull>(() => Joking());
            Before<Take>(() => Joking());
        }

        private bool Joking()
        {
            Print("Surely you're joking.");
            return true;
        }
    }
}
