﻿using ColossalCave.Places;
using Adventure.Net;

namespace ColossalCave.Things
{
    public class Forest : Object
    {
        public override void Initialize()
        {
            Name = "forest";
            Scenery = true;
            Synonyms.Are("forest", "tree", "trees", "oak", "maple", "grove", "pine", "spruce", "birch", "ash",
                "saplings", "bushes", "leaves", "berry", "berries", "hardwood");
            
            Description = "The trees of the forest are large hardwood oak and maple, " +
                          "with an occasional grove of pine or spruce. " +
                          "There is quite a bit of undergrowth, " +
                          "largely birch and ash saplings plus nondescript bushes of various sorts. " +
                          "This time of year visibility is quite restricted by all the leaves, " +
                          "but travel is quite easy if you detour around the spruce and berry bushes.";

            Attribute("multitude");

            FoundIn<EndOfRoad, HillInRoad, Valley, Forest1, Forest2>();
        }
    }
}

