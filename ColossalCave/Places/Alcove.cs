using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class Alcove : BelowGround
{
    public override void Initialize()
    {
        Name = "Alcove";

        Synonyms.Are("alcove");

        Description =
            "You are in an alcove. " +
            "A small northwest path seems to widen after a short distance. " +
            "An extremely tight tunnel leads east. " +
            "It looks like a very tight squeeze. " +
            "An eerie light can be seen at the other end.";

        NorthWestTo<MistyCavern>();

        EastTo(() =>
        {
            var carrying = Inventory.Items.Count;

            if (carrying == 0 || carrying == 1 && IsCarrying<EggSizedEmerald>())
            {
                return Room<PloverRoom>();
            }

            Print("Something you're carrying won't fit through the tunnel with you. You'd best take inventory and drop something.");
            return this;
        });
    }
}
