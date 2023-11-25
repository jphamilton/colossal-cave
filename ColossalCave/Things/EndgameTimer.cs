using Adventure.Net;
using ColossalCave.Actions;
using ColossalCave.Places;
using System.Linq;

namespace ColossalCave.Things;

public class EndgameTimer : Object
{
    public int TimeLeft { get; set; } = 25;

    public override void Initialize()
    {

        DaemonRunning = false;

        Daemon = () =>
        {
            TimeLeft--;

            if (TimeLeft > 0)
            {
                return;
            }

            DaemonRunning = false;

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
