﻿using Adventure.Net.Verbs;
using Adventure.Net;
using ColossalCave.Objects;
using ColossalCave.Verbs;

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

            // define shared Before<Enter> here rather than in the objects themselves
            bool cannotEnter()
            {
                Print("The stream flows out through a pair of 1 foot diameter sewer pipes. " +
                       "It would be advisable to use the exit.");
                return true;
            }

            Has<SetOfKeys>();
            Has<TastyFood>();
            Has<BrassLantern>();
            Has<Bottle>();
            Has<Stream>();
            Has<WellHouse>();
            Has<Spring>()
                .Before<Enter>(cannotEnter);
            Has<SewerPipes>()
                .Before<Enter>(cannotEnter);


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
