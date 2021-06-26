using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;

namespace ColossalCave.Actions
{
    public abstract class FeeFieFoeFoo : Verb
    {
        private static int feeFieCount = 0;

        protected bool Count(int i)
        {
            if (feeFieCount != i)
            {
                feeFieCount = 0;
                return Print("Get it right, dummy!");
            }

            if (feeFieCount++ == 3)
            {
                feeFieCount = 0;

                var insideBuilding = Rooms.Get<InsideBuilding>();
                var giantRoom = Rooms.Get<GiantRoom>();
                var eggs = Objects.Get<GoldenEggs>();

                if (giantRoom.Contains(eggs))
                {
                    return Print("Nothing happens.");
                }

                if (Inventory.Contains(eggs) || eggs.InRoom)
                {
                    Print("The nest of golden eggs has vanished!\n");
                }
                else
                {
                    Print("Done!");
                }

                if (Inventory.Contains(eggs))
                {
                    Score.Add(-5);
                }

                if (insideBuilding.Contains(eggs))
                {
                    Score.Add(-eggs.DepositPoints);
                }

                Move<GoldenEggs>.To<GiantRoom>();
                
                if (CurrentRoom.Is<GiantRoom>())
                {
                    Print("\n\nA large nest full of golden eggs suddenly appears out of nowhere!");
                }

            }
            else
            {
                Print("Ok.");
            }

            return true;
        }

        public class Fee : FeeFieFoeFoo
        {
            public Fee()
            {
                Name = "fee";
            }

            public bool Expects()
            {
                return Count(0);
            }
        }

        public class Fie : FeeFieFoeFoo
        {
            public Fie()
            {
                Name = "fie";
            }

            public bool Expects()
            {
                return Count(1);
            }
        }

        public class Foe : FeeFieFoeFoo
        {
            public Foe()
            {
                Name = "foe";
            }

            public bool Expects()
            {
                return Count(2);
            }

        }

        public class Foo : FeeFieFoeFoo
        {
            public Foo()
            {
                Name = "foo";
            }

            public bool Expects()
            {
                return Count(3);
            }
        }
    }
}
