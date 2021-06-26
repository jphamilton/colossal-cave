using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class FrontOfBarrenRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Front of Barren Room";
            Synonyms.Are("front", "of", "entrance", "to", "barren", "room");
            Description =
                "You are standing at the entrance to a large, barren room. " +
                "A sign posted above the entrance reads: \"Caution! Bear in room!\"";
            NoDwarf = true;

            WestTo<LimestonePassage>();
            UpTo<LimestonePassage>();
            EastTo<BarrenRoom>();
            InTo<BarrenRoom>();
        }
    }

    public class CautionSign : Scenic
    {
        public override void Initialize()
        {
            Name = "caution sign";
            Synonyms.Are("sign", "barren", "room", "caution");
            Description = "The sign reads, \"Caution! Bear in room!\"";

            FoundIn<FrontOfBarrenRoom>();
        }
    }
}
