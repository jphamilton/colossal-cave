using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class SewerPipes : Scenic
    {
        public override void Initialize()
        {
            Name = "pair of 1 foot diameter sewer pipes"; 
            Synonyms.Are("pipes", "pipe", "one", "foot", "diameter", "sewer", "sewer-pipes");
            Description = "Too small. The only exit is to the west.";

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

