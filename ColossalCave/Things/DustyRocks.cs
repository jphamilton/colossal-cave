using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class DustyRocks : Scenic
{
    public override void Initialize()
    {
        Name = "dusty rocks";
        Synonyms.Are("rocks", "boulders", "stones", "rock", "boulder", "stone", "dusty", "dirty");
        Description = "They're just rocks. (Dusty ones, that is.)";
        Multitude = true;

        FoundIn<DustyRockRoom>();

        Before<Push>(Nope);
        Before<Pull>(Nope);
        Before<LookUnder>(Nope);

    }

    private bool Nope()
    {
        return Print("You'd have to blast your way through.");
    }
}
