﻿
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Cobbles : Scenic
{
    public override void Initialize()
    {
        Name = "cobbles";
        Synonyms.Are("cobble", "cobbles", "cobblestones", "cobblestone", "stones", "stone");
        PluralName = true;
        Description = "They're just ordinary cobbles.";

        FoundIn<CobbleCrawl, DebrisRoom, BelowTheGrate>();
    }
}

