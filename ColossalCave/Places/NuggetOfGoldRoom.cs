using ColossalCave.Objects;

namespace ColossalCave.Places
{
    public class NuggetOfGoldRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "Low Room";
            Description = "This is a low room with a crude note on the wall:\r\n\r\n\"You won't get it up the steps\".";

            Has<LargeGoldNugget>();
            Has<CrudeNote>();

            NorthTo<HallOfMists>();
        }
    }
}

