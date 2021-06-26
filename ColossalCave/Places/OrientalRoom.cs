using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class OrientalRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "Oriental Room";
            Synonyms.Are("oriental", "room");
            Description =
                "This is the oriental room. " +
                "Ancient oriental cave drawings cover the walls. " +
                "A gently sloping passage leads upward to the north, another passage leads se, " +
                "and a hands and knees crawl leads west.";

            WestTo<LargeLowRoom>();
            SouthEastTo<SwissCheeseRoom>();
            UpTo<MistyCavern>();
            NorthTo<MistyCavern>();
        }
    }

    public class AncientOrientalDrawings : Scenic
    {
        public override void Initialize()
        {
            Name = "ancient oriental drawings";
            Synonyms.Are("paintings", "drawings", "art", "cave", "ancient", "oriental");
            Description = "They seem to depict people and animals.";
            
            Attribute("multitude");

            FoundIn<OrientalRoom>();
        }
    }
}
