using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class SlabRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "Slab Room";
            
            Synonyms.Are("slab", "room");
            
            Description = 
                "You are in a large low circular chamber " +
                "whose floor is an immense slab fallen from the ceiling (slab room). " +
                "East and west there once were large passages, but they are now filled with boulders. " +
                "Low small passages go north and south, and the south one quickly bends west around the boulders.";

            SouthTo<WestEndOfTwoPitRoom>();
            
            UpTo<SecretNSCanyon0>();
            
            NorthTo<Bedquilt>();

        }
    }
}



