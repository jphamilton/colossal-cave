using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public abstract class FissureRoom : BelowGround
    {
        public override void Initialize()
        {
            Before<Jump>(() =>
            {
                if (CurrentRoom.Has<CrystalBridge>())
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

    public class Fissure : Scenic
    {
        public override void Initialize()
        {
            Name = "fissure";
            Synonyms.Are("wide", "fissure");
            Description = "The fissure looks far too wide to jump.";

            FoundIn<WestSideOfFissure, EastBankOfFissure>();
        }
    }
}

