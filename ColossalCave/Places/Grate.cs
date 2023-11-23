using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Grate : Door
{
    public Grate()
    {
        Locked = true;
    }

    public override void Initialize()
    {
        Name = "steel grate";
        Synonyms.Are("grate", "lock", "gate", "grille", "metal", "string", "steel", "grating");
        Description = "It just looks like an ordinary grate mounted in concrete.";

        FoundIn<BelowTheGrate, OutsideGrate>();

        LocksWithKey<SetOfKeys>(Locked);

        Describe = () => Open ? "\nThe grate stands open." : !Locked ? "\nThe grate is unlocked but shut." : null;

        DoorDirection(() => In<BelowTheGrate>() ? Direction<Up>() : Direction<Down>());

        DoorTo(() => In<BelowTheGrate>() ? Room<OutsideGrate>() : Room<BelowTheGrate>());


    }
}

