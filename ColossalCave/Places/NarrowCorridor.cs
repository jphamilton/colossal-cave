using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class NarrowCorridor : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Narrow Corridor";
            Synonyms.Are("narrow", "corridor");
            Description = 
                "You are in a long, narrow corridor stretching out of sight to the west." +
                "At the eastern end is a hole through which you can see a profusion of leaves.";

            DownTo<WestPit>();
            //WestTo<GiantRoom>();
            EastTo<WestPit>();

            Before<Jump>(() =>
            {
                Print("You fall and break your neck!");
                AfterLife.Death();
                return true;
            });
        }
    }

    public class Leaves : Scenic
    {
        public override void Initialize()
        {
            Name = "leaves";
            Synonyms.Are("leaf", "leaves", "plant", "tree", "stalk", "beanstalk", "profusion");
            Description = "The leaves appear to be attached to the beanstalk you climbed to get here.";
            Article = "some";

            FoundIn<NarrowCorridor>();

            /*
            Before<Count>(() => {
                Print("69,105."); // Thanks again, Rene.
                return true;
            });
             */
        }
    }
}
