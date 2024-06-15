using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Places;

public class SecretEWCanyon : BelowGround
{
    public override void Initialize()
    {
        Name = "Secret E/W Canyon Above Tight Canyon";
        Synonyms.Are("secret", "e/w", "canyon", "above", "tight", "canyon");
        Description =
            "You are in a secret canyon which here runs E/W. " +
            "It crosses over a very tight canyon 15 feet below. " +
            "If you go down you may not be able to get back up.";

        EastTo<HallOfMtKing>();

        WestTo<SecretCanyon>();

        DownTo<NSCanyon>();

        Before<Go>((Direction direction) =>
        {
            if (direction is West)
            {
                Global.State.CanyonFrom = this;
            }

            return false;
        });
    }
}
