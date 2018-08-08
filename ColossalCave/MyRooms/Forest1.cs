using System;
using ColossalCave.MyObjects;
using Adventure.Net;

namespace ColossalCave.MyRooms
{
    public class Forest1 : AboveGround
    {
        public override void Initialize()
        {
            Name = "In Forest";
            Synonyms.Are("forest");
            Description = "You are in open forest, with a deep valley to one side.";

            EastTo<Valley>();
            DownTo<Valley>();
            NorthTo<Forest1>();
            WestTo<Forest1>();
            SouthTo<Forest1>();

            Has<Forest>();

            Initial = () =>
                {
                    var rnd = new Random(DateTime.Now.Second);
                    if (rnd.Next(1, 2) == 1)
                    {
                        var forest2 = Room<Forest2>();
                        L.MovePlayerTo(forest2);
                    }

                };
        }
    }
}


