using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace ColossalCave.Places;

[ExcludeFromCodeCoverage]
public class Bedquilt : BelowGround
{
    public override void Initialize()
    {
        Name = "Bedquilt";
        Synonyms.Are("bedquilt");
        Description =
            "You are in bedquilt, a long east/west passage with holes everywhere. " +
            "To explore at random select north, south, up, or down.";

        EastTo<ComplexJunction>();
        WestTo<SwissCheeseRoom>();
        SouthTo<SlabRoom>();
        UpTo<DustyRockRoom>();
        NorthTo<JunctionOfThree>();
        DownTo<Anteroom>();

        Before<Go>((Direction direction) =>
        {
            int destiny = 0;

            if ((direction is South || direction is Down) && Random.Number(1, 100) <= 80)
            {
                destiny = 1;
            }

            else if (direction is Up && Random.Number(1, 100) <= 50)
            {
                MovePlayer.To<SecretNSCanyon1>();
                return true;
            }

            else if (direction is North && Random.Number(1, 100) <= 60)
            {
                destiny = 1;
            }

            else if (direction is North && Random.Number(1, 100) <= 75)
            {

                MovePlayer.To<LargeLowRoom>();
                return true;
            }

            if (destiny == 1)
            {
                Print("You have crawled around in some little holes and wound up back in the main passage.");
                return true;
            }

            return false;
        });
    }
}
