using Adventure.Net;

namespace ColossalCave.Things
{
    public class HoleAbovePit : Scenic
    {
        public override void Initialize()
        {
            Name = "hole above pit";
            Synonyms.Are("hole", "large", "above", "pit");
            Description = "The hole is in the wall above the pit at this end of the room.";
        }
    }
}
