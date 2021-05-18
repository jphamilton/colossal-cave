using ColossalCave.Places;

namespace ColossalCave.Places
{
    public class Streambed : Scenic
    {
        public override void Initialize()
        {
            Name = "streambed";
            Synonyms.Are("bed", "streambed", "rock", "small", "rocky", "bare", "dry");
        }
    }
}

