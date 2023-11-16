using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class SecretCanyon : BelowGround
{
    public override void Initialize()
    {
        Name = "Secret Canyon";
        Synonyms.Are("secret", "canyon");
        Description = "You are in a secret canyon which exits to the north and east.";

        Before<Yes>(() =>
        {
            if (Dragon.IsBeingAttacked)
            {
                Dragon.Remove();

                var corpse = Objects.Get<DragonCorpse>();

                corpse.MoveToLocation();

                Dragon.IsBeingAttacked = false;

                Print("Congratulations! You have just vanquished a dragon with your bare hands! (Unbelievable, isn't it?)");
                return true;
            }

            return false;
        });

        Before<No>(() =>
        {
            if (Dragon.IsBeingAttacked)
            {
                Dragon.IsBeingAttacked = false;
                Print("I should think not.");
                return true;
            }

            return false;
        });

        EastTo(() =>
        {
            if (Global.CanyonFrom is SecretEWCanyon canyon)
            {
                return canyon;
            }

            if (Dragon.InRoom)
            {
                Print("The dragon looks rather nasty. You'd best not try to get by.");
                return this;
            }

            return Rooms.Get<SecretEWCanyon>();
        });

        NorthTo(() =>
        {
            if (Global.CanyonFrom is SecretNSCanyon canyon)
            {
                return canyon;
            }

            if (Dragon.InRoom)
            {
                Print("The dragon looks rather nasty. You'd best not try to get by.");
                return this;
            }

            return Rooms.Get<SecretEWCanyon>();
        });

        OutTo(() =>
        {
            return Global.CanyonFrom;
        });


    }

    private static Dragon Dragon
    {
        get
        {
            return Objects.Get<Dragon>();
        }
    }
}

