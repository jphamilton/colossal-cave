using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class SecretCanyon : BelowGround
{
    private Dragon Dragon => Objects.Get<Dragon>();

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

                return Print("Congratulations! You have just vanquished a dragon with your bare hands! (Unbelievable, isn't it?)");
            }

            return false;
        });

        Before<No>(() =>
        {
            if (Dragon.IsBeingAttacked)
            {
                Dragon.IsBeingAttacked = false;
                return Print("I should think not.");
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

        OutTo(() => Global.CanyonFrom);

    }

}
