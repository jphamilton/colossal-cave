using Adventure.Net;
using Adventure.Net.Actions;
using Adventure.Net.Things;
using ColossalCave.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class PloverRoom : BelowGround
{
    public override void Initialize()
    {
        Name = "Plover Room";

        Synonyms.Are("plover", "room");

        Description =
            "You're in a small chamber lit by an eerie green light. " +
            "An extremely narrow tunnel exits to the west. " +
            "A dark corridor leads northeast.";

        Light = true;

        NorthEastTo<DarkRoom>();

        WestTo(() =>
        {
            var carrying = Inventory.Items.Count;

            if (carrying == 0 || carrying == 1 && Player.IsCarrying<EggSizedEmerald>())
            {
                return Room<Alcove>();
            }

            Print("Something you're carrying won't fit through the tunnel with you. You'd best take inventory and drop something.");

            return this;
        });

        Before<Plover>(() =>
        {
            if (Player.IsCarrying<EggSizedEmerald>())
            {
                Move<EggSizedEmerald>.To<PloverRoom>();
                Score.Add(-5, true);
            }

            MovePlayer.To<Y2>();

            return true;
        });

        Before<Go>((Direction direction) =>
        {
            if (direction is Out)
            {
                MovePlayer.To(W());
                return true;
            }

            return false;
        });
    }
}
