using ColossalCave.Places;
using Adventure.Net.Actions;
using Adventure.Net;

namespace ColossalCave.Things
{
    public class WellHouse : Scenic
    {
        public override void Initialize()
        {
            Name = "well house";
            Synonyms.Are("well", "house", "brick", "building", "small", "wellhouse");
            Description = "It's a small brick building. It seems to be a well house.";

            Before<Enter>(() =>
                {
                    var insideBuilding = Room<InsideBuilding>();
                    
                    if (In<HillInRoad>() && !insideBuilding.Visited)
                    {
                        Print("It's too far away.");
                        return true;
                    }
                    
                    MovePlayer.To<InsideBuilding>();
                    return false;
                }
            );

        }
       
    }
}