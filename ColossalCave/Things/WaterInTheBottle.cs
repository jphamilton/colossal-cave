using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class WaterInTheBottle : Object
    {
        public override void Initialize()
        {
            Name = "bottled water"; 
            Synonyms.Are("bottled", "water", "h2o");
            Article = "some";
            Description = "It looks like ordinary water to me.";

            Before<Drink>(() =>
                {
                    var bottle = Get<Bottle>();
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