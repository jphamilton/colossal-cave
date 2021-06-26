using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class LimestonePassage : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Limestone Passage";
            Synonyms.Are("limestone", "passage");
            Description =
                "You are walking along a gently sloping north/south passage " +
                "lined with oddly shaped limestone formations.";
            NoDwarf = true;

            NorthTo<ForkInPath>();
            UpTo<ForkInPath>();
            SouthTo<FrontOfBarrenRoom>();
            DownTo<FrontOfBarrenRoom>();
        }
    }

    public class LimestoneFormation : Scenic
    {
        public override void Initialize()
        {
            Name = "limestone formations";
            Synonyms.Are("formations", "shape", "shapes", "lime", "limestone", "stone", "oddly", "shaped", "oddly-shaped");
            Description = "Every now and then a particularly strange shape catches your eye.";
            Attribute("multitude");

            FoundIn<LimestonePassage>();
        }
    }
}
