using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class WellHouse : Scenic
{
    public override void Initialize()
    {
        Name = "well house";
        Synonyms.Are("well", "house", "brick", "building", "small", "wellhouse");
        Description = "It's a small brick building. It seems to be a well house.";

        FoundIn<EndOfRoad, HillInRoad, InsideBuilding>();

        Before<Enter>(() =>
            {
                var insideBuilding = Room<InsideBuilding>();

                if (Player.Location is HillInRoad && !insideBuilding.Visited)
                {
                    return Print("It's too far away.");
                }

                return MovePlayer.To<InsideBuilding>();
            }
        );

    }

}