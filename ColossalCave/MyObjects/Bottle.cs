using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
{
    public class Bottle : Container
    {
        public override void Initialize()
        {
            Name = "small bottle";
            Synonyms.Are("bottle", "jar", "flask");
            IsOpen = true;
            InitialDescription = "There is an empty bottle here.";
            Article = "the";

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

            Before<Receive>(() =>
                {
                    if (Noun.Is<Stream>() || Noun.Is<Oil>())
                    {
                        Execute("fill bottle"); 
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
                var stream = Objects.Get<Stream>();
                var spring = Objects.Get<Spring>();
                var oil = Objects.Get<Oil>();

                if (stream.InScope || spring.InScope)
                {
                    IsTouched = true;
                    Add<WaterInTheBottle>();
                    Print("The bottle is now full of water.");
                }
                else if (oil.InScope)
                {
                    IsTouched = true;
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
