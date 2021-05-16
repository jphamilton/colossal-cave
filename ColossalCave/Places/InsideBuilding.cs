using ColossalCave.Objects;
using ColossalCave.Verbs;
using Adventure.Net;
using Adventure.Net.Verbs;

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
        
            
            Has<SetOfKeys>();
            Has<TastyFood>();
            Has<BrassLantern>();
            Has<Bottle>();
            Has<Stream>();
            Has<WellHouse>();
            Has<Spring>();
            Has<SewerPipes>();

            Before<Enter>(() =>
                {
                    if (Noun.Is<Spring>() || Noun.Is<SewerPipes>())
                    {
                        Print("The stream flows out through a pair of 1 foot diameter sewer pipes. " +
                               "It would be advisable to use the exit.");
                        return true;
                    }

                    return false;
                });

            Before<Xyzzy>(() =>
                {
                    var debrisRoom = Room<DebrisRoom>();
                    
                    if (debrisRoom.Visited)
                    {
                        Library.MovePlayerTo<DebrisRoom>();
                        return false;
                    }

                    Print(Library.DoNotUnderstand);
                    return true;
                });
        }
    }
}
