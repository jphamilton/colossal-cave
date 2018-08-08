using Adventure.Net.Verbs;
using Object=Adventure.Net.Object;

namespace ColossalCave.MyObjects
{
    public class BlackRod : Object
    {
        public override void Initialize()
        {
            Name = "black rod with a rusty star on the end";
            Description = "It's a three foot black rod with a rusty star on an end.";
            InitialDescription = "A three foot black rod with a rusty star on one end lies nearby.";

            Before<Wave>(() =>
                {
                    return true;
                });
        }
    }
}

//Object  -> black_rod "black rod with a rusty star on the end"
//  with  name 'rod' 'star' 'black' 'rusty' 'star' 'three' 'foot' 'iron',
//        description "It's a three foot black rod with a rusty star on an end.",
//        initial
//            "A three foot black rod with a rusty star on one end lies nearby.",
//        before [;
//          Wave:
//            if (location == West_Side_Of_Fissure or On_East_Bank_Of_Fissure) {
//                if (caves_closed) "Peculiar. Nothing happens.";
//                if (CrystalBridge notin nothing) {
//                    remove CrystalBridge;
//                    give CrystalBridge absent;
//                    West_Side_Of_Fissure.e_to = nothing;
//                    On_East_Bank_Of_Fissure.w_to = nothing;
//                    "The crystal bridge has vanished!";
//                }
//                else {
//                    move CrystalBridge to location;
//                    give CrystalBridge ~absent;
//                    West_Side_Of_Fissure.e_to = CrystalBridge;
//                    On_East_Bank_Of_Fissure.w_to = CrystalBridge;
//                    "A crystal bridge now spans the fissure.";
//                }
//            }
//            "Nothing happens.";
//        ];

