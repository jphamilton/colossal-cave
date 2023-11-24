using Adventure.Net;
using ColossalCave.Things;

namespace ColossalCave.Places;

public class DeadEnd13 : DeadEnd
{
    public override void Initialize()
    {
        base.Initialize();

        NoDwarf = true;

        Description = "This is the pirate's dead end.";

        SouthEastTo<AlikeMaze13>();

        OutTo<AlikeMaze13>();

        Initial = () =>
        {
            var pirate = Objects.Get<Pirate>();
            pirate.DaemonRunning = false;

            var chest = Objects.Get<TreasureChest>();

            if (chest.InRoom && !chest.Touched)
            {
                Print("You've found the pirate's treasure chest!");
            }
        };
    }
}

