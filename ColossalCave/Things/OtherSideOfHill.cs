using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class OtherSideOfHill : Scenic
    {
        public override void Initialize()
        {
            Name  = "other side of hill";
            Synonyms.Are("side", "other", "of");
            Description = "Why not explore it yourself?";

            FoundIn<HillInRoad>();
        }
    }
}

