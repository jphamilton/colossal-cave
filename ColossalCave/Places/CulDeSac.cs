using Adventure.Net;

namespace ColossalCave.Places;

public class CulDeSac : BelowGround
{
    public override void Initialize()
    {
        Name = "Cul-de-Sac";
        Synonyms.Are("cul-de-sac", "cul", "de", "sac");
        Description = "You are in a cul-de-sac about eight feet across.";

        UpTo<RaggedCorridor>();
        OutTo<RaggedCorridor>();
    }
}
