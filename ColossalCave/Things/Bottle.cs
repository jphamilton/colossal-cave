using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Extensions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Bottle : Container
{
    public override void Initialize()
    {
        Name = "small bottle";
        Synonyms.Are("bottle", "jar", "flask");
        Open = true;
        InitialDescription = "There is an empty bottle here.";

        FoundIn<InsideBuilding>();

        Before<Fill>(() =>
            {
                Fill();
                return true;
            }
        );

        Before<Empty>(() =>
            {
                if (IsEmpty)
                {
                    Print("The bottle is already empty!");
                }
                else
                {
                    Empty();
                    Print("Your bottle is now empty and the ground is now wet.");
                }

                return true;
            }
        );

        Receive((obj) =>
            {
                if (obj is Stream || obj is OilInTheBottle)
                {
                    Fill();
                    return true;
                }

                Print("The bottle is only supposed to hold liquids.");
                return false;
            });
    }

    public void Fill()
    {
        if (!IsEmpty)
        {
            Print("The bottle is full already.");
        }
        else
        {
            var stream = Get<Stream>();
            var spring = Get<Spring>();
            var oil = Get<PoolOfOil>();

            if (stream.InScope || spring.InScope)
            {
                Touched = true; // TODO: possible bug - WaterInTheBottle should be Touched?
                Add<WaterInTheBottle>();
                Print("The bottle is now full of water.");
            }
            else if (oil.InScope)
            {
                Touched = true; // TODO: possible bug - OilInTheBottle should be Touched?
                Add<OilInTheBottle>(); 
                Print("The bottle is now full of oil.");
            }
            else
            {
                Print("There is nothing here with which to fill the bottle.");
            }
        }
    }
}