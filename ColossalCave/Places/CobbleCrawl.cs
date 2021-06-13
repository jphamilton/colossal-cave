﻿using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class CobbleCrawl : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Cobble Crawl";
            Synonyms.Are("cobble", "crawl");
            Description = "You are crawling over cobbles in a low passage. There is a dim light at the east end of the passage.";
            HasLight = true;

            EastTo<BelowTheGrate>();
            WestTo<DebrisRoom>();

            Has<WickerCage>();
            Has<Cobbles>();
        }
    }
}
