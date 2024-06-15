using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Extensions;
using ColossalCave.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Stream : Scenic
{
    public override void Initialize()
    {
        Name = "stream";
        Synonyms.Are("stream", "water", "brook", "river", "lake", "small", "tumbling",
                     "splashing", "babbling", "rushing", "reservoir");

        FoundIn<EndOfRoad, Valley, SlitInStreambed, InsideBuilding, Reservoir, InPit, CavernWithWaterfall>();

        Before<Drink>(() =>
            {
                Print("You have taken a drink from the stream. " +
                      "The water tastes strongly of minerals, but is not unpleasant. " +
                      "It is extremely cold.");
                return true;
            });

        Before<Take>(() =>
            {
                var bottle = Get<Bottle>();

                if (!Inventory.Contains(bottle))
                {
                    Print("You have nothing in which to carry the water.");
                }
                else
                {
                    bottle.Fill();
                }

                return true;
            });

        Before<Insert>(() =>
        {
            if (Second is Bottle bottle)
            {
                bottle.Fill();
            }
            else
            {
                Print("You have nothing in which to carry the water.");
            }

            return true;
        });

        Receive((obj) =>
            {
                if (obj is Bottle)
                {
                    var bottle = Objects.Get<Bottle>();
                    bottle.Fill();
                    return true;
                }

                if (obj is MingVase)
                {
                    obj.Remove();
                    Objects.Get<Shards>().MoveToLocation();
                    Score.Add(-5);
                    Print("The sudden change in temperature has delicately shattered the vase.");
                    return true;
                }

                obj.Remove();

                if (obj is Treasure)
                {
                    Score.Add(-5);
                    Print($"{obj.DName.Capitalize} washes away with the stream.");
                }

                return true;
            });
    }
}

