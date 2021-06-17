using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class DustyRocks : Scenic
    {
        public override void Initialize()
        {
            Name = "dusty rocks";
            Synonyms.Are("rocks", "boulders", "stones", "rock", "boulder", "stone", "dusty", "dirty");
            Description = "They're just rocks. (Dusty ones, that is.)";
            // has multitude

            FoundIn<DustyRockRoom>();

            Before<Push>(Nope);
            Before<Pull>(Nope);
            Before<LookUnder>(Nope);
            
        }

        private bool Nope()
        {
            Print("You'd have to blast your way through.");
            return true;
        }
    }
}
