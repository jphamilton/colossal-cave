using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
{
    public class VendingMachine : Scenic
    {
        public override void Initialize()
        {
            Name = "vending machine";
            Synonyms.Are("machine", "slot", "vending", "massive", "battery", "coin");
            Description = "The instructions on the vending machine read, \"Insert coins to receive fresh batteries.\"";

            Before<Receive>(() =>
                {
                    //            if (noun == rare_coins) {
                    //                move fresh_batteries to location;
                    //                remove rare_coins;
                    //                "Soon after you insert the coins in the coin slot,
                    //                 the vending machine makes a grinding sound, and a set of fresh batteries falls at your feet.";
                    //            }
                    //            "The machine seems to be designed to take coins.";
                    return true;
                });
        }
    }
}

//Scenic  -> VendingMachine "vending machine"
//  with  name 'machine' 'slot' 'vending' 'massive' 'battery' 'coin',
//        description
//            "The instructions on the vending machine read,
//             ~Insert coins to receive fresh batteries.~",
//        before [;
//          Receive:
//            if (noun == rare_coins) {
//                move fresh_batteries to location;
//                remove rare_coins;
//                "Soon after you insert the coins in the coin slot,
//                 the vending machine makes a grinding sound, and a set of fresh batteries falls at your feet.";
//            }
//            "The machine seems to be designed to take coins.";
//          Attack:
//            "The machine is quite sturdy and survives your attack without getting so much as a scratch.";
//          LookUnder:
//            "You don't find anything under the machine.";
//          Search:
//            "You can't get inside the machine.";
//          Take:
//            "The vending machine is far too heavy to move.";
//        ];

