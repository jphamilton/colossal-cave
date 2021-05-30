using Adventure.Net;
using Adventure.Net.Verbs;
using ColossalCave.Places;

namespace ColossalCave.Objects
{
    public class Grate : Door
    {
        public override void Initialize()
        {
            Name = "steel grate";
            Synonyms.Are("grate", "lock", "gate", "grille", "metal", "string", "steel", "grating");
            Description = "It just looks like an ordinary grate mounted in concrete.";
            Article = "the";

            LocksWithKey<SetOfKeys>(true);

            DoorDirection = () =>
                {
                    if (In<BelowTheGrate>())
                        return Direction<Up>();
                    return Direction<Down>();
                };

            DoorTo = () =>
                {
                    if (In<BelowTheGrate>())
                    {
                        return Room<OutsideGrate>();
                    }
                    return Room<BelowTheGrate>();
                };

            Describe = () =>
                {
                    if (IsOpen)
                       return "\nThe grate stands open.";
                    if (!IsLocked)
                       return "\nThe grate is unlocked but shut.";
                    return null;
                };
        }
    }
}

