using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Places;

public class SecretNSCanyon0 : BelowGround
{
    public override void Initialize()
    {
        Name = "Secret N/S Canyon";
        Synonyms.Are("secret", "n/s", "canyon");
        Description = "You are in a secret N/S canyon above a large room.";

        DownTo<SlabRoom>();
        SouthTo<SecretCanyon>();
        NorthTo<MirrorCanyon>();

        Before<Go>((Direction direction) =>
        {
            if (direction is South)
            {
                Global.State.CanyonFrom = this;
            }

            return false;
        });
    }
}
