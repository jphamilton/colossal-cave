using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;

namespace ColossalCave.Places
{
    public class TopOfSmallPit : BelowGround
    {
        public override void Initialize()
        {
            Name = "At Top of Small Pit";
            
            Synonyms.Are("top", "of", "small", "pit");
            
            Description =
                "At your feet is a small pit breathing traces of white mist. " +
                "A west passage ends here except for a small crack leading on.\n\n" +
                "Rough stone steps lead down the pit.";

            NoDwarf = true;

            EastTo<BirdChamber>();
           
            WestTo(()=>
                {
                    Print("That crack is far too small for you to follow.");
                    return this;
                });

            DownTo(() =>
                {
                    if (Inventory.Contains<LargeGoldNugget>())
                    {
                        //deadflag = 1;
                        Print("You are at the bottom of the pit with a broken neck.");
                        return this;

                        // TODO: death?
                    }

                    return Rooms.Get<HallOfMists>(); 
              
                });
            
        }
    }
}
