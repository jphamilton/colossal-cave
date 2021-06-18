using Adventure.Net;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class EggSizedEmerald : Treasure
    {
        public override void Initialize()
        {
            Name = "emerald the size of a plover's egg";
            Synonyms.Are("emerald", "egg-sized", "egg", "sized", "plover's");
            Description = "Plover's eggs, by the way, are quite large.";
            Article = "an";
            DepositPoints = 14;

            FoundIn<PloverRoom>();
        }
    }
}
