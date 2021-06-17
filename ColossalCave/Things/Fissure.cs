using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class Fissure : Scenic
    {
        public override void Initialize()
        {
            Name = "fissure";
            Synonyms.Are("wide", "fissure");
            Description = "The fissure looks far too wide to jump.";

            FoundIn<WestSideOfFissure, EastBankOfFissure>();
        }
    }
}
