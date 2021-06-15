﻿using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class Stalactite : Scenic
    {
        public override void Initialize()
        {
            Name = "stalactite";
            Synonyms.Are("stalactite", "stalagmite", "stalagtite", "large");
            Description = "You could probably climb down it, but you can forget coming back up.";

            Before<LookUnder>(() => GetAGrip());
            Before<Push>(() => GetAGrip());
            Before<Take>(() => GetAGrip());
        }

        private bool GetAGrip()
        {
            Print("Do get a grip on yourself.");
            return true;
        }
    }
}