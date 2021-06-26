using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class GiantRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "Giant Room";
            Synonyms.Are("giant", "room");
            Description =
                "You are in the giant room. " +
                 "The ceiling here is too high up for your lamp to show it. " +
                 "Cavernous passages lead east, north, and south. " +
                 "On the west wall is scrawled the inscription, \"Fee fie foe foo\" [sic].";
            
            SouthTo<NarrowCorridor>();
            EastTo<RecentCaveIn>();
            NorthTo<ImmenseNSPassage>();
        }
    }

    public class ScrawledInscription : Scenic
    {
        public override void Initialize()
        {
            Name = "scrawled inscription";
            Synonyms.Are("inscription", "writing", "scrawl", "scrawled");
            Description = "It says, \"Fee fie foe foo [sic].\"";

            FoundIn<GiantRoom>();
        }
    }
}
