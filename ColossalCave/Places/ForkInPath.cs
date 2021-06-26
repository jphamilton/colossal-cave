using Adventure.Net;

namespace ColossalCave.Places
{
    public class ForkInPath : BelowGround
    {
        public override void Initialize()
        {
            Name = "At Fork in Path";
            Synonyms.Are("fork, in, path");
            Description =
                "The path forks here. The left fork leads northeast. " +
                "A dull rumbling seems to get louder in that direction. " +
                "The right fork leads southeast down a gentle slope. " +
                "The main corridor enters from the west.";
            
            NoDwarf = true;

            WestTo<Corridor>();
            NorthEastTo<JunctionWithWarmWalls>();
            //SouthEastTo<LimestonePassage>();
            //DownTo<LimestonePassage>();

        }
    }
}
