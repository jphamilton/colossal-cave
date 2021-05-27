﻿using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.Places
{
    public class OldBatteries : Item
    {
        public override void Initialize()
        {
            Name = "worn-out batteries";
            Synonyms.Are("batteries, battery, worn, out, worn-out");
            Description = "They look like ordinary batteries.";
            InitialDescription = "Some worn-out batteries have been discarded nearby.";
            
            Before<Count>(() =>
                {
                    Print("A pair.");
                    return true;
                });
        }
    }

}
