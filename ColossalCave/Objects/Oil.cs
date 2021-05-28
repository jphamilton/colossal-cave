using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.Objects
{
    public class Oil : Scenic
    {
        public override void Initialize()
        {
            Name = "pool of oil";
            Synonyms.Are("pool", "oil", "small");
            Description = "It looks like ordinary oil.";

            Before<Drink>(() =>
                {
                    Print("Absolutely not.");
                    return true;
                });

            Before<Take>(() =>
                {
                    var bottle = Get<Bottle>();

                    if (bottle.InInventory)
                    {
                        // TODO: not sure I like all the object checking in Fill
                        bottle.Fill();
                    }
                    else
                    {
                        Print("You have nothing in which to carry the oil.");
                    }
                    
                    return true;
                });

         
        }
    }
}

