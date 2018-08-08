using ColossalCave.MyObjects;
using ColossalCave.MyVerbs;
using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyRooms
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
        
            Has<Spring>();
            Has<SewerPipes>();
            Has<SetOfKeys>();
            Has<TastyFood>();
            Has<BrassLantern>();
            Has<Bottle>();
            Has<WellHouse>();
            Has<Stream>();

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
                        L.MovePlayerTo<DebrisRoom>();
                        return false;
                    }

                    Print(L.DoNotUnderstand);
                    return true;
                });
        }
    }
}
