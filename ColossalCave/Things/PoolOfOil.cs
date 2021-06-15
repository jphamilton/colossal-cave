﻿using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class PoolOfOil : Scenic
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
                var bottle = Bottle;

                if (!bottle.InInventory)
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
                if (IndirectObject is Bottle)
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

        private Bottle Bottle
        {
            get
            {
                return Objects.Get<Bottle>();
            }
        }
    }
}