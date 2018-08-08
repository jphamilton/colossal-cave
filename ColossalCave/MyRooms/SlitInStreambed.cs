using ColossalCave.MyObjects;
using Adventure.Net.Verbs;

namespace ColossalCave.MyRooms
{
    public class SlitInStreambed : AboveGround
    {
        public override void Initialize()
        {
            Name = "At Slit In Streambed";
            Synonyms.Are("slit", "in", "streambed");
            Description = "At your feet all the water of the stream splashes into a 2-inch slit in the rock. Downstream the streambed is bare rock.";
        
            NorthTo<Valley>();
            EastTo<Forest1>();
            WestTo<Forest1>();
            SouthTo<OutsideGrate>();

            Before<Down>(() =>
                {
                    Print("You don't fit through a two-inch slit!");
                    return true;
                });

            Before<In>(() =>
                {
                    Print("You don't fit through a two-inch slit!");
                    return true;
                });

            Has<TwoInchSlit>();
            Has<Stream>();
        }
    }
}

 