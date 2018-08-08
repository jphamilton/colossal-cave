
using ColossalCave.MyObjects;

namespace ColossalCave.MyRooms
{
    public class Valley : AboveGround
    {
        public override void Initialize()
        {
            Name = "In A Valley";
            Description = "You are in a valley in the forest beside a stream tumbling along a rocky bed.";

            NorthTo<EndOfRoad>();
            EastTo<Forest1>();
            WestTo<Forest1>();
            UpTo<Forest1>();
            SouthTo<SlitInStreambed>();
            DownTo<SlitInStreambed>();

            Has<Stream>();
            Has<Forest>();
            Has<Streambed>();

        }
    }
}

