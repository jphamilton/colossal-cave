using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
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
                    if (!Player.Has<Bottle>())
                    {
                        Print("You have nothing in which to carry the oil.");
                    }
                    else
                    {
                        Execute("fill bottle");
                    }
                    return true;
                });

         
        }
    }
}

