using System;
using ColossalCave.MyObjects;

namespace ColossalCave.MyRooms
{
    public class Forest2 : AboveGround
    {
        public override void Initialize()
        {
            Name = "In Forest";
            Description = "You are in open forest near both a valley and a road.";
        
            NorthTo<EndOfRoad>();
            EastTo<Valley>();
            WestTo<Valley>();
            DownTo<Valley>();
            SouthTo<Forest1>();

            Has<Road>();
            Has<Forest>();
        }
    }
}


