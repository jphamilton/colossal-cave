using Adventure.Net;

namespace ColossalCave.Places
{
    public class EastBankOfFissure : FissureRoom
    {
        public override void Initialize()
        {
            base.Initialize();

            Name = "On East Bank of Fissure";
            Synonyms.Are("east", "e", "bank", "side", "of", "fissure");
            Description = "You are on the east bank of a fissure slicing clear across the hall. The mist is quite thick here, and the fissure is too wide to jump.";

            EastTo<HallOfMists>();

            WestTo(CannotCross);
        }

        public void BridgeAppears()
        {
            Contents.Add(Get<CrystalBridge>());
            WestTo<CrystalBridge>();
        }

        public void BridgeDisappears()
        {
            Contents.Remove(Get<CrystalBridge>());
            WestTo(CannotCross);
        }
    }
}
