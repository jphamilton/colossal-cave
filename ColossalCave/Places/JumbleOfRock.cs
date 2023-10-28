using Adventure.Net;

namespace ColossalCave.Places;

public class JumbleOfRock : BelowGround
{
    public override void Initialize()
    {
        Name = "Jumble of Rock";
        Synonyms.Are("jumble", "of", "rock");
        Description = "";

        DownTo<Y2>();
        UpTo<HallOfMists>();
    }
}
