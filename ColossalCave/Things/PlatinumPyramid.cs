using Adventure.Net;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class PlatinumPyramid : Treasure
    {
        public override void Initialize()
        {
            Name = "platinum pyramid";
            Synonyms.Are("platinum", "pyramid", "platinum", "pyramidal");
            Description = "The platinum pyramid is 8 inches on a side!";
            InitialDescription = "There is a platinum pyramid here, 8 inches on a side!";
            DepositPoints = 14;
            
            FoundIn<DarkRoom>();
        }
    }
}