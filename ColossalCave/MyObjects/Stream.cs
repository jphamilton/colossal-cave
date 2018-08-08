using ColossalCave.MyRooms;
using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
{
    public class Stream : Scenic
    {
        public override void Initialize()
        {
            Name = "stream";
            Synonyms.Are("stream", "water", "brook", "river", "lake", "small", "tumbling",
                         "splashing", "babbling", "rushing", "reservoir");

            //TODO: FoundIn<InPit>();
            //TODO: FoundIn<InCavernWithWaterfall>();
            //TODO: FoundIn<AtReservoir>();

            Before<Drink>(() =>
                {
                    Print("You have taken a drink from the stream. " +
                          "The water tastes strongly of minerals, but is not unpleasant. " +
                          "It is extremely cold.");
                    return true;
                });

            Before<Take>(() =>
                {
                    var bottle = Objects.Get<Bottle>();
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


            Before<Receive>(() =>
                {
                    if (Noun.Is<Bottle>())
                    {
                        var bottle = Objects.Get<Bottle>();
                        bottle.Fill();
                        return true;
                    }

                    return false;
                });
        }
    }
}


//TODO: Scenic  Stream "stream"
//0117    with  name 'stream' 'water' 'brook' 'river' 'lake' 'small' 'tumbling'
//0118               'splashing' 'babbling' 'rushing' 'reservoir',
//0119          found_in At_End_Of_Road In_A_Valley At_Slit_In_Streambed In_Pit
//0120                   In_Cavern_With_Waterfall At_Reservoir Inside_Building,
//0121          before [;
//0130            Insert:
//0131              if (second == bottle) <<Fill bottle>>;
//0132              "You have nothing in which to carry the water.";
//0133            Receive:
//0134              if (noun == ming_vase) {
//0135                  remove ming_vase;
//0136                  move shards to location;
//0137                  score = score - 5;
//0138                  "The sudden change in temperature has delicately shattered the vase.";
//0139              }
//0140              if (noun == bottle) <<Fill bottle>>;
//0141              remove noun;
//0142              if (noun ofclass Treasure) score = score - 5;
//0143              print_ret (The) noun, " washes away with the stream.";
//0144          ];

