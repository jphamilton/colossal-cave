using Adventure.Net;
using ColossalCave.Actions;
using ColossalCave.Places;
using System.Linq;

namespace ColossalCave.Things;

public class CaveCloser : Object
{
    public override void Initialize()
    {
        Daemon = () =>
        {
            if (Global.TreasuresFound < Global.MaxTreasures)
            {
                return;
            }

            DaemonStarted = false;

            Global.CavesClosed = true;

            Score.Add(25, true);

            Rooms.Get<CrystalBridge>().Remove();

            //Get<CrystalBridge>().Remove();

            var grate = Rooms.Get<Grate>();
            grate.Locked = true;
            grate.Open = false;

            Get<SetOfKeys>().Remove();

            Get<Dwarf>().DaemonStarted = false;
            Get<Pirate>().DaemonStarted = false;

            Get<BurlyTroll>().Remove();
            Get<Bear>().Remove();
            Get<Dragon>().Remove();

            Get<EndgameTimer>().DaemonStarted = true;

            Print("\nA sepulchral voice reverberating through the cave says, \"Cave " +
                "closing soon. All adventurers exit immediately through main office.\"");

        };
    }
}

public class EndgameTimer : Object
{
    public int TimeLeft = 25;

    public override void Initialize()
    {

        DaemonStarted = false;

        Daemon = () =>
        {
            TimeLeft--;

            if (TimeLeft > 0)
            {
                return;
            }

            DaemonStarted = false;

            Score.Add(10, true);

            foreach (var obj in Inventory.Items.ToList())
            {
                obj.Remove();
            }

            var bottle = Get<Bottle>();
            bottle.Empty();

            bottle.MoveTo<NeEnd>();

            Move<GiantClam>.To<NeEnd>();
            Move<BrassLantern>.To<NeEnd>();
            Move<BlackRod>.To<NeEnd>();
            Move<LittleBird>.To<SwEnd>();
            Move<VelvetPillow>.To<SwEnd>();

            Print(
                "\nThe sepulchral voice intones, \"The cave is now closed.\" " +
                "As the echoes fade, there is a blinding flash of light " +
                "(and a small puff of orange smoke). . . " +
                "\n\n " +
                "As your eyes refocus, you look around...\n"
            );

            MovePlayer.To<NeEnd>();
        };
    }
}
