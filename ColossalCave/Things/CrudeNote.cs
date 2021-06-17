using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class CrudeNote : Scenic
    {
        public override void Initialize()
        {
            Name = "note";
            Synonyms.Are("note", "crude");
            Description = "The note says, \"You won't get it up the steps\".";

            FoundIn<NuggetOfGoldRoom>();
        }
    }
}
