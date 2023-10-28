using Adventure.Net;
using ColossalCave.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class SetOfKeys : Object
{
    public override void Initialize()
    {
        Name = "set of keys";
        Synonyms.Are("keys", "key", "keyring", "set", "of", "bunch");
        Description = "It's just a normal-looking set of keys.";
        InitialDescription = "There are some keys on the ground here.";

        FoundIn<InsideBuilding>();

        Before<Count>(() =>
        {
            return Print("A dozen or so keys.");
        });

    }
}

