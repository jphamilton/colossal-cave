using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Sign : Object
{
    public override void Initialize()
    {
        Name = "sign";
        Synonyms.Are("sign", "witt", "company", "construction");
        InitialDescription =
            "A sign in midair here says \"Cave under construction beyond this point. " +
            "Proceed at own risk. [Witt Construction Company]\"";
        Static = true;

        FoundIn<Anteroom>();

        Before<Take>(() => Print("It's hanging way above your head."));
    }
}
