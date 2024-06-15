using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class BrassLantern : Object
{
    public int PowerRemaining { get; set; }

    public BrassLantern()
    {
        On = false;
        DaemonRunning = true;
        PowerRemaining = 330;
    }

    public override void Initialize()
    {
        Name = "brass lantern";
        Synonyms.Are("lamp", "headlamp", "headlight", "lantern", "light", "shiny", "brass");
        Switchable = true;

        FoundIn<InsideBuilding>();

        Describe = () =>
            {
                if (On)
                {
                    return "Your lamp is here, gleaming brightly.";
                }

                return "There is a shiny brass lamp nearby.";
            };

        Daemon = () =>
        {
            if (!On)
            {
                DaemonRunning = false;
                return;
            }

            int t = --PowerRemaining;

            if (t == 0)
            {
                Light = false;
                On = false;
            }

            var freshBatteries = Objects.Get<FreshBatteries>();

            if (Inventory.Contains(this) || InScope)
            {
                string message = null;

                if (t == 0)
                {
                    message = "\nYour lamp has run out of power.";

                    if (!Inventory.Contains(freshBatteries) && !Player.Location.Light)
                    {
                        // TODO: deadflag = 3;
                        message += " You can't explore the cave without a lamp. So let's call it a day.";
                        Print(message);
                        Dead();
                        return;
                    }
                    else
                    {
                        message += ReplaceBatteries();
                        Print(message);
                    }

                    Print("\n");
                    return;
                }

                if (t == 30)
                {
                    message = "\nYour lamp is getting dim.";

                    if (freshBatteries.HaveBeenUsed)
                    {
                        message += " You're also out of spare batteries. You'd best start wrapping this up.";
                        Print(message);
                        return;
                    }

                    if (freshBatteries.InVendingMachine && Room<DeadEnd14>().Visited)
                    {
                        message += " You'd best start wrapping this up, " +
                            "unless you can find some fresh batteries. " +
                            "I seem to recall there's a vending machine in the maze. " +
                            "Bring some coins with you.";
                        Print(message);
                        return;
                    }

                    if (!freshBatteries.InVendingMachine && !freshBatteries.InScope)
                    {
                        message += " You'd best go back for those batteries.";
                    }

                    Print(message);
                    Print("\n");
                    return;
                }


            }
        };

        Before<Examine>(() =>
        {
            string result = "It is a shiny brass lamp";

            if (!On)
            {
                result += ". It is not currently lit.";
            }
            else if (PowerRemaining < 30)
            {
                result += ", glowing dimly.";
            }
            else
            {
                result += ", glowing brightly.";
            }

            return Print(result);
        });

        Before<Rub>(() => "Rubbing the electric lamp is not particularly rewarding. Anyway, nothing exciting happens.");

        Receive((obj) =>
        {
            if (obj is OldBatteries)
            {
                return "Those batteries are dead; they won't do any good at all.";
            }
            else if (obj is FreshBatteries)
            {
                return ReplaceBatteries();
            }

            return "The only thing you might successfully put in the lamp is a fresh pair of batteries.";
        });

        Before<SwitchOn>(() =>
        {
            if (PowerRemaining <= 0)
            {
                return Print("Unfortunately, the batteries seem to be dead.");
            }

            return false;
        });

        After<SwitchOn>(() =>
        {
            Light = true;
            DaemonRunning = true;
        });

        After<SwitchOff>(() => Light = false);
    }

    private string ReplaceBatteries()
    {
        var fresh = Get<FreshBatteries>();

        if (fresh.InScope)
        {
            fresh.Remove();
            fresh.HaveBeenUsed = true;

            var old = Get<OldBatteries>();

            old.MoveToLocation();

            PowerRemaining = 2500;

            return "I'm taking the liberty of replacing the batteries.";
        }

        return "";
    }

}
