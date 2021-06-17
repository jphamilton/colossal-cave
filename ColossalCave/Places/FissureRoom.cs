using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Places
{
    public abstract class FissureRoom : BelowGround
    {
        public override void Initialize()
        {
            Before<Jump>(() =>
            {
                var room = CurrentRoom.Location;
                
                if (room.Contains<CrystalBridge>())
                {
                    Print("I respectfully suggest you go across the bridge instead of jumping.");
                    return true;
                }

                Print("You didn't make it.");
                
                AfterLife.GoTo();
                
                return true;
            });

            DownTo(() =>
            {
                Output.Print("The fissure is too terrifying!");
                return this;
            });
        }

        protected Room CannotCross()
        {
            Output.Print("The fissure is too wide.");
            return this;
        }
    }
}

