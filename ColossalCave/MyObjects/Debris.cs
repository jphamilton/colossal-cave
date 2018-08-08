using ColossalCave.MyRooms;

namespace ColossalCave.MyObjects
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
