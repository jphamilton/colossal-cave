using Adventure.Net;

namespace ColossalCave.Places
{
    public class SteepIncline : BelowGround
    {
        public override void Initialize()
        {
            Name = "Steep Incline Above Large Room";
            Synonyms.Are("steep", "incline", "above", "large", "room");
            Description =
                "You are at the top of a steep incline above a large room. " +
                "You could climb down here, but you would not be able to climb up. " +
                "There is a passage leading back to the north.";

            NorthTo<CavernWithWaterfall>();
            DownTo<LargeLowRoom>();
        }
    }
}
