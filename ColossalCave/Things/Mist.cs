﻿using ColossalCave.Places;

namespace ColossalCave.Things;

public class Mist : Scenic
{
    public override void Initialize()
    {
        Name = "mist";
        Synonyms.Are("mist", "vapor", "wisps", "white");
        Description =
            "Mist is a white vapor, usually water, seen from time to time in caverns. " +
            "It can be found anywhere but is frequently a sign of a deep pit leading down to water.";

        FoundIn<TopOfSmallPit, HallOfMists, EastBankOfFissure, WestEndOfHallOfMists, MirrorCanyon, Reservoir, WindowOnPit2>();
        FoundIn<WindowOnPit1, MistyCavern, SwSideOfChasm>();
    }
}
