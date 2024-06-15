using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace ColossalCave.Things;

public class WaterInTheBottle : Object
{
    public override void Initialize()
    {
        Name = "bottled water";
        Synonyms.Are("bottled", "water", "h2o");
        IndefiniteArticle = "some";
        Description = "It looks like ordinary water to me.";

        Before<Drink>(() =>
            {
                var bottle = Get<Bottle>();

                if (bottle.Children.Contains(this))
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