using Adventure.Net;
using ColossalCave.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class CaveCloser : Object
{
    public override void Initialize()
    {
        Daemon = () =>
        {
            if (Global.State.TreasuresFound < Global.MaxTreasures)
            {
                return;
            }

            DaemonRunning = false;

            Global.State.CavesClosed = true;

            Score.Add(25, true);

            Rooms.Get<CrystalBridge>().Remove();

            var grate = Rooms.Get<Grate>();
            grate.Locked = true;
            grate.Open = false;

            Get<SetOfKeys>().Remove();

            Get<Dwarf>().DaemonRunning = false;
            Get<Pirate>().DaemonRunning = false;

            Get<BurlyTroll>().Remove();
            Get<Bear>().Remove();
            Get<Dragon>().Remove();

            Get<EndgameTimer>().DaemonRunning = true;

            Print("\nA sepulchral voice reverberating through the cave says, \"Cave " +
                "closing soon. All adventurers exit immediately through main office.\"");

        };
    }
}
