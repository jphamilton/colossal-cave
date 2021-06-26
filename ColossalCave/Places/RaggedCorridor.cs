using Adventure.Net;

namespace ColossalCave.Places
{
    public class RaggedCorridor : BelowGround
    {
        public override void Initialize()
        {
            Name = "Ragged Corridor";
            Synonyms.Are("ragged", "corridor");
            Description = "You are in a long sloping corridor with ragged sharp walls.";

            UpTo<ShellRoom>();
            DownTo<CulDeSac>();
        }
    }
}
