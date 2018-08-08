using ColossalCave.MyObjects;
using Adventure.Net;

namespace ColossalCave.MyRooms
{
    public class EndOfRoad : AboveGround
    {
        public override void Initialize()
        {
            Name = "At End Of Road";
            Synonyms.Are("end, of, road, street, path, gully");
            Description = "You are standing at the end of a road before a small brick building. " +
                          "Around you is a forest. " +
                          "A small stream flows out of the building and down a gully.";

            WestTo<HillInRoad>();
            EastTo<InsideBuilding>();
            DownTo<Valley>();
            SouthTo<Valley>();
            NorthTo<Forest1>();
            InTo<InsideBuilding>();

            Has<Road>();
            Has<WellHouse>();
            Has<Stream>();
            Has<Forest>();
        }
    }
}