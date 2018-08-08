using ColossalCave.MyRooms;

namespace ColossalCave.MyObjects
{
    public class Road : Scenic
    {
        public override void Initialize()
        {
            Name = "road";
            Synonyms.Are("road", "street", "path", "dirt");
            Description = "The road is dirt, not yellow brick.";
        }
    }
}

