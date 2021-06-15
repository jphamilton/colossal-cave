﻿using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class WestEndOfTwoPitRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "At West End of Twopit Room";
            Synonyms.Are("twopit", "room");
            Description =
                "You are at the west end of the twopit room. " +
                "There is a large hole in the wall above the pit at this end of the room.";

            Has<HoleAbovePit>();
            // Has<PlantStickingUp>(); // absent at first

            EastTo<EastEndOfTwoPitRoom>();
            
            WestTo<SlabRoom>();
            
            DownTo<WestPit>();
            
            UpTo(() =>
            {
                Print("It is too far up for you to reach.");
                return this;
            });

            Before<Enter>(() =>
            {
                if (CurrentObject is HoleAbovePit)
                {
                    Print("It is too far up for you to reach.");
                }

                return false;
            });
        }
    }
}


