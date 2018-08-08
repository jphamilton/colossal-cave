using System.Linq;
using ColossalCave.MyObjects;
using Adventure.Net.Verbs;

namespace ColossalCave.MyRooms
{
    public class OutsideGrate : AboveGround
    {
        public override void Initialize()
        {
            Name = "Outside Grate";
            Description = "You are in a 20-foot depression floored with bare dirt. " + 
                          "Set into the dirt is a strong steel grate mounted in concrete. " + 
                          "A dry streambed leads into the depression.";
            
            EastTo<Forest1>();
            WestTo<Forest1>();
            SouthTo<Forest1>();
            NorthTo<SlitInStreambed>();

            Has<TwentyFootDepression>();
            Has<Grate>();

            DownTo(() =>
                       {
                           Grate grate = Get<Grate>();
                           if (!grate.IsLocked && !grate.IsOpen)
                           {
                               Print("(first opening the grate)");
                               grate.IsOpen = true;
                           }
                           return grate; //Room<BelowTheGrate>();
                       });
            
            //d_to [;
            //    if (Grate hasnt locked && Grate hasnt open) {
            //        print "(first opening the grate)^";
            //        give Grate open;
            //    }
            //    return Grate;
            //];
        
        }
    }
}

