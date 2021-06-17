using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class DustyRockRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Dusty Rock Room";
            Synonyms.Are("dusty", "rock", "room");
            Description = 
                "You are in a large room full of dusty rocks. " +
                "There is a big hole in the floor. " +
                "There are cracks everywhere, and a passage leading east.";

            EastTo<DirtyPassage>();

            DownTo<ComplexJunction>();
        }
    }
}

