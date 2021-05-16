using ColossalCave.Places;

namespace ColossalCave.Objects
{
    public class Debris : Scenic
    {
        public override void Initialize()
        {
            Name = "debris";
            Synonyms.Are("debris", "stuff", "mud");
            Description = "Yuck.";
        }
    }
}
