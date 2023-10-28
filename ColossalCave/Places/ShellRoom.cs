using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class ShellRoom : BelowGround
{
    public override void Initialize()
    {
        Name = "Shell Room";
        Synonyms.Are("shell, room");
        Description =
            "You're in a large room carved out of sedimentary rock. " +
            "The floor and walls are littered with bits of shells imbedded in the stone. " +
            "A shallow passage proceeds downward, and a somewhat steeper one leads up. " +
            "A low hands and knees passage enters from the south.";

        UpTo<ArchedHall>();

        DownTo<RaggedCorridor>();

        SouthTo(() =>
        {
            var clam = Get<GiantClam>();

            if (Inventory.Contains(clam))
            {
                if (clam.HasBeenOpened)
                {
                    Print("You can't fit this five-foot oyster through that little passage!");
                }
                else
                {
                    Print("You can't fit this five-foot clam through that little passage!");
                }

                return this;
            }

            return Rooms.Get<ComplexJunction>();
        });
    }
}
