﻿using Adventure.Net;
using Adventure.Net.ActionRoutines;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class PoolOfOil : Scenic
{
    public override void Initialize()
    {
        Name = "pool of oil";
        Synonyms.Are("pool", "oil", "small");
        Description = "It looks like ordinary oil.";

        FoundIn<EastPit>();

        Before<Drink>(() => Print("Absolutely not."));

        Before<Take>(() =>
        {
            var bottle = Bottle;

            if (Inventory.Contains(bottle))
            {
                bottle.Fill();
            }
            else
            {
                Print("You have nothing in which to carry the oil.");
            }

            return true;
        });


        Before<Insert>(() =>
        {
            var bottle = Bottle;
            if (Second is Bottle)
            {
                Bottle.Fill();
            }
            else
            {
                Print("You have nothing in which to carry the oil.");
            }

            return true;
        });
    }

    private Bottle Bottle => Objects.Get<Bottle>();
}
