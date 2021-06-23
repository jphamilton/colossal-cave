using Adventure.Net;
using Adventure.Net.Extensions;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class Bottle : Container
    {
        public override void Initialize()
        {
            Name = "small bottle";
            Synonyms.Are("bottle", "jar", "flask");
            Article = "the";
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
                    if (obj.Is<Stream>() || obj.Is<Oil>())
                    {
                        Fill();
                    }
                    else
                    {
                        Print("The bottle is only supposed to hold liquids.");
                    }

                    return true;
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
                var oil = Get<Oil>();

                if (stream.InScope || spring.InScope)
                {
                    Touched = true;
                    Add<WaterInTheBottle>();
                    Print("The bottle is now full of water.");
                }
                else if (oil.InScope)
                {
                    Touched = true;
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
}

//Object  -> bottle "small bottle"
//  with  name 'bottle' 'jar' 'flask',
//        initial "There is an empty bottle here.",
//        before [;
//          LetGo:
//            if (noun in bottle)
//                "You're holding that already (in the bottle).";
//          Receive:
//            if (noun == stream or Oil)
//                <<Fill self>>;
//            else
//                "The bottle is only supposed to hold liquids.";
//        ],
//  has   container open;
