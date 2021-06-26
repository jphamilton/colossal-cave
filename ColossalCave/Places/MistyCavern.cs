using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class MistyCavern : BelowGround
    {
        public override void Initialize()
        {
            Name = "Misty Cavern";
            Synonyms.Are("misty", "cavern");
            Description =
                "You are following a wide path around the outer edge of a large cavern. " +
                "Far below, through a heavy white mist, strange splashing noises can be heard. " +
                "The mist rises up through a fissure in the ceiling. " +
                "The path exits to the south and west.";

            SouthTo<OrientalRoom>();

            WestTo<Alcove>();
        }
    }

    public class MistyFissure : Scenic
    {
        public override void Initialize()
        {
            Name = "fissure";
            Synonyms.Are("fissure", "ceiling");
            Description = "You can't really get close enough to examine it.";

            FoundIn<MistyCavern>();
        }
    }
}
