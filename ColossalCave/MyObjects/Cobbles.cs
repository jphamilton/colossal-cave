
using ColossalCave.MyRooms;

namespace ColossalCave.MyObjects
{
    public class Cobbles : Scenic
    {
        public override void Initialize()
        {
            Name = "cobbles";
            Synonyms.Are("cobble", "cobbles", "cobblestones", "cobblestone", "stones", "stone");
            HasPluralName = true;
            Description = "They're just ordinary cobbles.";
        }
    }
}

