using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.Places
{
    public class WaterInTheBottle : Item
    {
        public override void Initialize()
        {
            Name = "water"; // used to be "bottled water"
            Synonyms.Are("bottled", "water", "h2o");
            Article = "some";
            Description = "It looks like ordinary water to me.";

            Before<Drink>(() =>
                {
                    var bottle = Adventure.Net.Objects.Get<Bottle>() as Container;
                    if (bottle.Contents.Contains(this))
                    {
                        
                        bottle.Remove<WaterInTheBottle>();
                        Print("You drink the cool, refreshing water, draining the bottle in the process.");
                    }
                    else
                    {
                        Print("Refreshing!");
                    }
                    return true;
                });
        }
    }
}