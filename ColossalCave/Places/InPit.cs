using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class InPit : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Pit";
            Synonyms.Are("in", "pit");
            Description = 
                "You are in the bottom of a small pit with a little stream, " +
                "which enters and exits through tiny slits."; ;
            
            NoDwarf = true;

            UpTo<BrinkOfPit>();
            
            DownTo(() =>
            {
                Print("You don't fit through the tiny slits!");
                return this;
            });
        }

        public class TinySlits : Scenic
        {
            public override void Initialize()
            {
                Name = "tiny slits";
                Synonyms.Are("slit", "slits", "tiny");
                // has multitude
                FoundIn<InPit>();
            }
        }
    }
}
