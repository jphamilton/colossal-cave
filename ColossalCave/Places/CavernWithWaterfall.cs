using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class CavernWithWaterfall : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Cavern With Waterfall";
            Synonyms.Are("cavern", "with", "waterfall");
            Description =
                "You are in a magnificent cavern with a rushing stream, " +
                "which cascades over a sparkling waterfall into a roaring whirlpool " +
                "which disappears through a hole in the floor. " +
                "Passages exit to the south and west.";

            SouthTo<ImmenseNSPassage>();
            //WestTo<SteepIncline>();

           
        }
    }

    public class Waterfall : Scenic
    {
        public override void Initialize()
        {
            Name = "waterfall";
            Synonyms.Are("waterfall", "whirlpool", "sparkling", "whirling");
            Description = "Wouldn't want to go down in in a barrel!";
            
            FoundIn<CavernWithWaterfall>();
        }
    }

    public class JeweledTrident : Treasure
    {
        public override void Initialize()
        {
            Name = "jeweled trident";
            Synonyms.Are("trident", "jeweled", "jewel-encrusted", "encrusted", "fabulous");
            Description = "The trident is covered with fabulous jewels!";
            InitialDescription = "There is a jewel-encrusted trident here!";
            DepositPoints = 14;

            FoundIn<CavernWithWaterfall>();
        }
    }
}
