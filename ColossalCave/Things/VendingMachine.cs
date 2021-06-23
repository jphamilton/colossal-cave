using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class VendingMachine : Scenic
    {
        public override void Initialize()
        {
            Name = "vending machine";
            Synonyms.Are("machine", "slot", "vending", "massive", "battery", "coin");
            Description = "The instructions on the vending machine read, \"Insert coins to receive fresh batteries.\"";

            FoundIn<DeadEnd14>();

            Receive((obj) =>
            {
                if (obj is RareCoins)
                {
                    Move<FreshBatteries>.Here();
                    obj.Remove();

                    return "Soon after you insert the coins in the coin slot, the vending machine makes a grinding sound, and a set of fresh batteries falls at your feet.";
                }

                return "The machine seems to be designed to take coins.";
            });

            Before<Attack>(() => "The machine is quite sturdy and survives your attack without getting so much as a scratch.");

            Before<LookUnder>(() => "You don't find anything under the machine.");

            Before<Search>(() => "You can't get inside the machine.");

            Before<Take>(() => "The vending machine is far too heavy to move.");
        }
    }
}

