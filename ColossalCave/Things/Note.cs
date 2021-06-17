using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class Note : Scenic
    {
        public override void Initialize()
        {
            Name = "note";
            Description = "The note says \"Magic word XYZZY\"";

            FoundIn<DebrisRoom>();
        }
    }
}
