using Adventure.Net;
using Adventure.Net.Extensions;
using Adventure.Net.Actions;
using ColossalCave.Places;
using ColossalCave.Actions;

namespace ColossalCave.Things
{
    public class Stream : Scenic
    {
        public override void Initialize()
        {
            Name = "stream";
            Synonyms.Are("stream", "water", "brook", "river", "lake", "small", "tumbling",
                         "splashing", "babbling", "rushing", "reservoir");

            FoundIn<EndOfRoad, Valley, SlitInStreambed, InsideBuilding, Reservoir, InPit>();
            // In_Cavern_With_Waterfall 

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
                    
                    if (!bottle.InInventory)
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
                if (IndirectObject is Bottle)
                {
                    ((Bottle)IndirectObject).Fill();
                }
                else
                {
                    Print("You have nothing in which to carry the water.");
                }

                return true;
            });

            Receive((obj) =>
                {
                    if (obj.Is<Bottle>())
                    {
                        var bottle = Objects.Get<Bottle>();
                        bottle.Fill();
                        return true;
                    }

                    if (obj.Is<MingVase>())
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
                        Print($"{obj.Article.TitleCase()} washes away with the stream."); // TODO: print_ret(The) noun, " washes away with the stream."
                    }

                    return true;
                });
        }
    }
}

