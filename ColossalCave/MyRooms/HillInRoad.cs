using ColossalCave.MyObjects;

namespace ColossalCave.MyRooms
{
    public class HillInRoad : AboveGround
    {
        public override void Initialize()
        {
            Name = "At Hill In Road";

            Description = "You have walked up a hill, still in the forest. " +
                          "The road slopes back down the other side of the hill. " +
                          "There is a building in the distance.";

            Has<Hill>();
            Has<OtherSideOfHill>();
            Has<Road>();
            Has<WellHouse>();
            Has<Forest>();

            EastTo<EndOfRoad>();
            NorthTo<EndOfRoad>();
            DownTo<EndOfRoad>();
            SouthTo<Forest1>();
        }
    }
}