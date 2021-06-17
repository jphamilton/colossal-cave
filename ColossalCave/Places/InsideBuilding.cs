using Adventure.Net;
using ColossalCave.Actions;

namespace ColossalCave.Places
{
    public class InsideBuilding : AboveGround
    {
        public override void Initialize()
        {
            Name = "Inside Building";
            Description = "You are inside a building, a well house for a large spring.";
            CantGo = "The stream flows out through a pair of 1 foot diameter sewer pipes. The only exit is to the west.";
        
            WestTo<EndOfRoad>();
            OutTo<EndOfRoad>();

            // TODO: Check "enter" - should output "the pipes are too small";

            // TODO: Try to move this here
            //before[;
            //    Enter:
            //        if (noun == Spring or SewerPipes)
            //            "The stream flows out through a pair of 1 foot diameter sewer pipes.
            //             It would be advisable to use the exit.";

            
            InTo(() =>
            {
                Print("The pipes are too small");
                return this;
            });

            Before<Plugh>(() =>
            {
                if (!Room<Y2>().Visited)
                {
                    return false;
                }

                MovePlayer.To<Y2>();
                
                return true;
            });

            Before<Xyzzy>(() =>
                {
                    var debrisRoom = Room<DebrisRoom>();
                    
                    if (debrisRoom.Visited)
                    {
                        MovePlayer.To<DebrisRoom>();
                        return false;
                    }

                    Print(Messages.DoNotUnderstand);
                    return true;
                });
        }
    }
}
