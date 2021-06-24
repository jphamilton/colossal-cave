using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class HillInRoad : AboveGround
    {
        public override void Initialize()
        {
            Name = "At Hill In Road";

            Description = "You have walked up a hill, still in the forest. " +
                          "The road slopes back down the other side of the hill. " +
                          "There is a building in the distance.";

            EastTo<EndOfRoad>();
            NorthTo<EndOfRoad>();
            DownTo<EndOfRoad>();
            SouthTo<Forest1>();
        }
    }

    public class Hill : Scenic
    {
        public override void Initialize()
        {
            Name = "hill";
            Synonyms.Are("hill", "bump", "incline");
            Description = "It's just a typical hill.";

            FoundIn<HillInRoad>();
        }
    }

    public class OtherSideOfHill : Scenic
    {
        public override void Initialize()
        {
            Name = "other side of hill";
            Synonyms.Are("side", "other", "of");
            Description = "Why not explore it yourself?";
            Article = "the";

            FoundIn<HillInRoad>();
        }
    }
}
