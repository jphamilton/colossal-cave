﻿using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class PitCrack : Scenic
    {
        public override void Initialize()
        {
            Name = "crack";
            Synonyms.Are("crack", "small");
            Description = "The crack is very small -- far too small for you to follow.";

            Before<Enter>(() =>
            {
                Print("The crack is far too small for you to follow.");
                return true;
            });
        }
    }

}