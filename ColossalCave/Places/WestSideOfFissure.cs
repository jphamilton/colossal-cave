﻿using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class WestSideOfFissure : FissureRoom
    {
        public override void Initialize()
        {
            base.Initialize();

            Name = "West Side of Fissure";
            Synonyms.Are("west", "w", "bank", "side", "of", "fissure");
            Description = "You are on the west side of the fissure in the hall of mists.";

            Has<Diamonds>();

            WestTo<WestEndOfHallOfMists>();
            
            EastTo(CannotCross);

            NorthTo(() =>
            {
                Output.Print("You have crawled through a very low wide passage parallel to and north of the hall of mists.\r\n");
                return Rooms.Get<WestEndOfHallOfMists>();
            });

        }

        public void BridgeAppears()
        {
            Contents.Add(Get<CrystalBridge>());
            EastTo<CrystalBridge>();
        }
        public void BridgeDisappears()
        {
            Contents.Remove(Get<CrystalBridge>());
            EastTo(CannotCross);
        }

    }
}
