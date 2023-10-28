using Adventure.Net;
using Adventure.Net.Actions;
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
        InitialDescription = "There are a few recent issues of ~Spelunker Today~ magazine here.";
        IndefiniteArticle = "a few";
        // multitude

        FoundIn<Anteroom>();

        After<Take>(() =>
        {
            if (CurrentRoom.Location is WittsEnd)
            {
                Score.Add(-1);
            }
        });

        After<Drop>(() =>
        {
            if (CurrentRoom.Location is WittsEnd)
            {
                Score.Add(1);
                Print("You really are at wit's end.");
            }
        });
    }
}
