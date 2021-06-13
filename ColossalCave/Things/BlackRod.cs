using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class BlackRod : Item
    {
        public override void Initialize()
        {
            Name = "black rod with a rusty star on the end";
            Synonyms.Are("rod", "star", "black", "rusty", "star", "three", "foot", "iron");
            Description = "It's a three foot black rod with a rusty star on an end.";
            InitialDescription = "A three foot black rod with a rusty star on one end lies nearby.";

            Before<Wave>(() =>
            {
                var room = CurrentRoom.Location;
                var westSideOfFissure = Rooms.Get<WestSideOfFissure>();
                var eastBankOfFissure = Rooms.Get<EastBankOfFissure>();
                var crystalBridge = Rooms.Get<CrystalBridge>();

                if (room == westSideOfFissure || room == eastBankOfFissure)
                {
                    // TODO: caves closed
                    // if (caves_closed) "Peculiar. Nothing happens.";

                    if (room.Contains(crystalBridge))
                    {
                        westSideOfFissure.BridgeDisappears();
                        eastBankOfFissure.BridgeDisappears();
                        Print("The crystal bridge has vanished!");
                    }
                    else
                    {
                        westSideOfFissure.BridgeAppears();
                        eastBankOfFissure.BridgeAppears();
                        Print("A crystal bridge now spans the fissure.");
                    }

                    return true;
                }

                Print("Nothing happens.");

                return true;
            });
        }
    }
}

