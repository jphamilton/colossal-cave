﻿using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using ColossalCave.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class SpelunkerToday : Object
{
    public override void Initialize()
    {
        Name = "recent issues of \"Spelunker Today\"";
        Synonyms.Are("magazines", "magazine", "issue", "issues", "spelunker", "today");
        Description = "I'm afraid the magazines are written in Dwarvish.";
        InitialDescription = "There are a few recent issues of \"Spelunker Today\" magazine here.";
        IndefiniteArticle = "a few";
        Multitude = true;

        FoundIn<Anteroom>();

        After<Take>(() =>
        {
            if (Player.Location is WittsEnd)
            {
                Score.Add(-1);
            }
        });

        After<Drop>(() =>
        {
            if (Player.Location is WittsEnd)
            {
                Score.Add(1, true);
                Print("You really are at wit's end.");
            }
        });
    }
}
