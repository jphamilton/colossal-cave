using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
{
    public class BrassLantern : Object
    {
        public int PowerRemaining { get; set; }

        private FreshBatteries freshBatteries = Objects.Get<FreshBatteries>();

        public override void Initialize()
        {
            Name = "brass lantern";
            Synonyms.Are("lamp", "headlamp", "headlight", "lantern", "light", "shiny", "brass");
            IsOn = false;
            IsSwitchable = true;
            DaemonStarted = true;
            PowerRemaining = 330;

            Describe = () =>
                           {
                               if (IsOn)
                                   return "Your lamp is here, gleaming brightly.";
                               return "There is a shiny brass lamp nearby.";
                           };

            Daemon = () =>
                {
                    if (!IsOn)
                    {
                        DaemonStarted = false;
                        return;
                    }

                    int t = PowerRemaining - 1;
                    if (t == 0)
                    {
                        HasLight = false;
                        IsOn = false;
                    }

                    if (InScope)
                    {
                        string result = null;

                        if (t == 0)
                        {
                            result = "Your lamp has run out of power. ";
                            if (freshBatteries.InInventory && !Location.HasLight)
                            {
                                // deadflag = 3;
                                result += "You can't explore the cave without a lamp. So let's call it a day.";
                                Print(result);
                            }
                            else
                            {
                                result += ReplaceBatteries();
                                Print("\n");
                            }

                            Print(result);
                        }
                        
                        if (t == 30) 
                        {
                            result = "Your lamp is getting dim.";
                            if (freshBatteries.HaveBeenUsed)
                            {
                                result += " You're also out of spare batteries. You'd best start wrapping this up.";
                            }

//                    if (fresh_batteries in VendingMachine && Dead_End_14 has visited)
//                        " You'd best start wrapping this up,
//                         unless you can find some fresh batteries.
//                         I seem to recall there's a vending machine in the maze.
//                         Bring some coins with you.";
//                    if (fresh_batteries notin VendingMachine or player or location)
//                        " You'd best go back for those batteries.";
//                    new_line;
//                    rtrue;
                        }         

                        
                    }
                };

            Before<Examine>(() =>
                {
                    
                    string result = "It is a shiny brass lamp";
                    if (!IsOn)
                        result += ". It is not currently lit.";
                    else if (PowerRemaining < 30)
                        result += ", glowing dimly.";
                    else
                        result += ", glowing brightly.";
                    Print(result);
                    return true;
                });

            Before<Rub>(() =>
                {
                    Print("Rubbing the electric lamp is not particularly rewarding. Anyway, nothing exciting happens.");
                    return true;
                });

            Before<Receive>(() =>
                {
                    if (Noun.Is<OldBatteries>())
                    {
                        Print("Those batteries are dead; they won't do any good at all.");
                    }
                    else if (Noun.Is<FreshBatteries>())
                    {
                        Print(ReplaceBatteries());
                    }
                    else
                    {
                        Print("The only thing you might successfully put in the lamp is a fresh pair of batteries.");
                    }
                    return true;
                });

            Before<SwitchOn>(() =>
                {
                    if (PowerRemaining <= 0)
                    {
                        Print("Unfortunately, the batteries seem to be dead.");
                        return true;
                    }
                    return false;
                });

            After<SwitchOn>(() =>
                {
                    HasLight = true;
                    DaemonStarted = true;
                    return false;
                });

            After<SwitchOff>(() =>
                {
                    HasLight = false;
                    return false;
                });
        }

        private string ReplaceBatteries()
        {
            var fresh = Objects.Get<FreshBatteries>();
            if (fresh.InScope)
            {
                fresh.Remove();
                fresh.HaveBeenUsed = true;
                var old = Objects.Get<OldBatteries>();
                Location.Objects.Add(old);
                PowerRemaining = 2500;
                return "I'm taking the liberty of replacing the batteries.";
            }

            return null;
        }

    }
}

//TODO: Object  -> brass_lantern "brass lantern"
//  with  name 'lamp' 'headlamp' 'headlight' 'lantern' 'light' 'shiny' 'brass',
//        when_off "There is a shiny brass lamp nearby.",
//        when_on "Your lamp is here, gleaming brightly.",
//        daemon [ t;
//            if (self hasnt on) {
//                StopDaemon(self);
//                rtrue;
//            }
//            t = --(self.power_remaining);
//            if (t == 0) give self ~on ~light;
//            if (self in player || self in location) {
//                if (t == 0) {
//                    print "Your lamp has run out of power.";
//                    if (fresh_batteries notin player && location hasnt light) {
//                        deadflag = 3;
//                        " You can't explore the cave without a lamp.
//                         So let's just call it a day.";
//                    }
//                    else
//                        self.replace_batteries();
//                    new_line;
//                    rtrue;
//                }
//                if (t == 30) {
//                    print "Your lamp is getting dim.";
//                    if (fresh_batteries.have_been_used)
//                        " You're also out of spare batteries.
//                         You'd best start wrapping this up.";
//                    if (fresh_batteries in VendingMachine && Dead_End_14 has visited)
//                        " You'd best start wrapping this up,
//                         unless you can find some fresh batteries.
//                         I seem to recall there's a vending machine in the maze.
//                         Bring some coins with you.";
//                    if (fresh_batteries notin VendingMachine or player or location)
//                        " You'd best go back for those batteries.";
//                    new_line;
//                    rtrue;
//                }
//            }
//        ],
//        before [;
//          Burn:
//            <<SwitchOn self>>;
//          SwitchOn:
//            if (self.power_remaining <= 0)
//                "Unfortunately, the batteries seem to be dead.";
//          Receive:
//            if (noun == old_batteries)
//                "Those batteries are dead; they won't do any good at all.";
//            if (noun == fresh_batteries) {
//                self.replace_batteries();
//                rtrue;
//            }
//            "The only thing you might successfully put in the lamp
//             is a fresh pair of batteries.";
//        ],
//        after [;
//          SwitchOn:
//            give self light;
//            StartDaemon(self);
//          SwitchOff:
//            give self ~light;
//        ],
//        replace_batteries [;
//            if (fresh_batteries in player or location) {
//                remove fresh_batteries;
//                fresh_batteries.have_been_used = true;
//                move old_batteries to location;
//                self.power_remaining = 2500;
//                "I'm taking the liberty of replacing the batteries.";
//            }
//        ],
//        power_remaining 330,
//  has   switchable;
