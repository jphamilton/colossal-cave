using ColossalCave.Places;

namespace ColossalCave.Things;

public class BarsOfSilver : Treasure
{
    public override void Initialize()
    {
        Name = "bars of silver";
        Synonyms.Are("silver", "bars");
        Description = "They're probably worth a fortune!";
        IndefiniteArticle = "some";
        InitialDescription = "There are bars of silver here!";

        FoundIn<LowNSPassage>();
    }
}
