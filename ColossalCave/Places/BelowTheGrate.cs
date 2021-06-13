using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class BelowTheGrate : BelowGround
    {
        public override void Initialize()
        {
            Name = "Below the Grate";
            Synonyms.Are("below", "grate");
            Description = "You are in a small chamber beneath a 3x3 steel grate to the surface. " + 
                          "A low crawl over cobbles leads inward to the west.";
            
            WestTo<CobbleCrawl>();
            UpTo<Grate>();
            
            HasLight = true;

            Has<Cobbles>();
            Has<Grate>();
        }
    }
}


