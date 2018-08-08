using ColossalCave.MyObjects;
using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyRooms
{
    public class TopOfSmallPit : AdventRoom
    {
        public override void Initialize()
        {
            Name = "At Top of Small Pit";
            Synonyms.Are("top", "of", "small", "pit");
            Description =
                "At your feet is a small pit breathing traces of white mist. " +
                "A west passage ends here except for a small crack leading on.\n\n" +
                "Rough stone steps lead down the pit.";

            EastTo<BirdChamber>();
           
            WestTo(()=>
                {
                    Print("That crack is far too small for you to follow.");
                    return this;
                });

            DownTo(() =>
                {
                    if (Player.Has<LargeGoldNugget>())
                    {
                        //deadflag = 1;
                        Print("You are at the bottom of the pit with a broken neck.");
                        return null;
                    }

                    return null; // HallOfMists
              
                });

            Before<Enter>(() =>
                {
                    if (Noun.Is<PitCrack>())
                    {
                        Print("The crack is far too small for you to follow.");
                        return true;
                    }

                    return false;
                });

            Has<SmallPit>();
            Has<PitCrack>();
            Has<Mist>();
        }
    }

//Room    At_Top_Of_Small_Pit "At Top of Small Pit"
//with  name 'top' 'of' 'small' 'pit',
//    description
//        "At your feet is a small pit breathing traces of white mist.
//         A west passage ends here except for a small crack leading on.
//         ^^
//         Rough stone steps lead down the pit.",
//    e_to In_Bird_Chamber,
//    w_to "The crack is far too small for you to follow.",
//    d_to [;
//        if (large_gold_nugget in player) {
//            deadflag = 1;
//            "You are at the bottom of the pit with a broken neck.";
//        }
//        return In_Hall_Of_Mists;
//    ],
//    before [;
//      Enter:
//        if (noun == PitCrack)
//            "The crack is far too small for you to follow.";
//    ],
//has   nodwarf;

}
