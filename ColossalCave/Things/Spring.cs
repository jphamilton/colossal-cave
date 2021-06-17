using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class Spring : Scenic
    {
        public override void Initialize()
        {
            Name = "spring"; 
            Synonyms.Are("spring", "large");
            Description = "The stream flows out through a pair of 1 foot diameter sewer pipes.";

            FoundIn<InsideBuilding>();

            Before<Enter>(() =>
            {
                Print("The stream flows out through a pair of 1 foot diameter sewer pipes. " +
                       "It would be advisable to use the exit.");
                return true;
            });
        }
    }
}

