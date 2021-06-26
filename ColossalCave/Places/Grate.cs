using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class Grate : Door
    {
        public override void Initialize()
        {
            Name = "steel grate";
            Synonyms.Are("grate", "lock", "gate", "grille", "metal", "string", "steel", "grating");
            Description = "It just looks like an ordinary grate mounted in concrete.";
            Article = "the";

            FoundIn<BelowTheGrate, OutsideGrate>();

            LocksWithKey<SetOfKeys>(true);

            Describe = () =>
            {
                if (Open)
                    return "\nThe grate stands open.";
                if (!Locked)
                    return "\nThe grate is unlocked but shut.";
                return null;
            };

            DoorDirection(() =>
            {
                if (In<BelowTheGrate>())
                    return Direction<Up>();
                return Direction<Down>();
            });

            DoorTo(() =>
            {
                if (In<BelowTheGrate>())
                {
                    return Room<OutsideGrate>();
                }
                return Room<BelowTheGrate>();
            });

            
        }
    }
}

