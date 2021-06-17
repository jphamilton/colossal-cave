using Adventure.Net;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class Y2Rock : Scenic, ISupporter
    {
        public override void Initialize()
        {
            Name = "rock";
            Synonyms.Are("rock", "y2");
            Description = "There is a large \"Y2\" painted on the rock.";

            FoundIn<Y2>();
        }
    }
}
