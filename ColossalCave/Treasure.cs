using System;
using Object = Adventure.Net.Object;

namespace ColossalCave
{
    public abstract class Treasure : Object
    {
        public int DepositPoints { get; protected set; }
        
        protected Treasure()
        {
            DepositPoints = 10;    
        }
        //public override void Initialize()
        //{
        //    throw new NotImplementedException();
        //}
    }

//Class   Treasure
//with  after [;
//  Take:
//    if (location == Inside_Building)
//        score = score - self.depositpoints;
//    score = score + 5;
//    if (noun hasnt treasure_found) {
//        give noun treasure_found;
//        treasures_found++;
//        score = score + 2;
//    }
//    "Taken!";
//  Insert:
//    score = score - 5;  ! (in case put inside the wicker cage)
//  Drop:
//    score = score - 5;
//    if (location == Inside_Building) {
//        score = score + self.depositpoints;
//        "Safely deposited.";
//    }
//],
//depositpoints 10;

}
