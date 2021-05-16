using ColossalCave.Places;

namespace ColossalCave.Objects
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

