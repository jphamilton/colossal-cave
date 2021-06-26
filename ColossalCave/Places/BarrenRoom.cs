using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class BarrenRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Barren Room";
            Synonyms.Are("in", "barren", "room");
            Description =
                "You are inside a barren room. " +
                "The center of the room is completely empty except for some dust. " +
                "Marks in the dust lead away toward the far end of the room. " +
                "The only exit is the way you came in.";
            NoDwarf = true;

            WestTo<FrontOfBarrenRoom>();
            OutTo<FrontOfBarrenRoom>();

        }
    }

    public class Dust : Scenic
    {
        public override void Initialize()
        {
            Name = "dust";
            Synonyms.Are("dust", "marks");
            Description = "It just looks like ordinary dust.";
            FoundIn<BarrenRoom>();
        }
    }
}

