using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Actions;
using ColossalCave.Things;

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

            Before<Enter>(() =>
            {
                if (Noun is Spring || Noun is SewerPipes)
                {
                    Print("The stream flows out through a pair of 1 foot diameter sewer pipes. " +
                       "It would be advisable to use the exit.");
                    return true;
                }

                return false;
            });

            InTo(() =>
            {
                Print("The pipes are too small.");
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
                if (Room<DebrisRoom>().Visited)
                {
                    return MovePlayer.To<DebrisRoom>();
                }

                return Print(Messages.DoNotUnderstand);
            });
        }
    }

    public class Spring : Scenic
    {
        public override void Initialize()
        {
            Name = "spring";
            Synonyms.Are("spring", "large");
            Description = "The stream flows out through a pair of 1 foot diameter sewer pipes.";

            FoundIn<InsideBuilding>();
        }
    }

    public class SewerPipes : Scenic
    {
        public override void Initialize()
        {
            Name = "pair of 1 foot diameter sewer pipes";
            Synonyms.Are("pipes", "pipe", "one", "foot", "diameter", "sewer", "sewer-pipes");
            Description = "Too small. The only exit is to the west.";

            FoundIn<InsideBuilding>();
        }
    }
}
