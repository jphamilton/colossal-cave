using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using ColossalCave.Places;
using ColossalCave.Things;

namespace ColossalCave.Actions;

public abstract class FeeFieFoeFoo : Routine
{
    private static int feeFieCount = 0;

    private bool Handle()
    {
        feeFieCount = 0;

        var insideBuilding = Rooms.Get<InsideBuilding>();
        var giantRoom = Rooms.Get<GiantRoom>();
        var eggs = Objects.Get<GoldenEggs>();

        if (eggs.Parent == giantRoom)
        {
            return Fail("Nothing happens.");
        }

        if (Inventory.Contains(eggs) || eggs.InRoom)
        {
            Print("The nest of golden eggs has vanished!\n");
        }
        else
        {
            Print("Done!");
        }

        if (Inventory.Contains(eggs))
        {
            Score.Add(-5);
        }

        if (eggs.Location == insideBuilding)
        {
            Score.Add(-eggs.DepositPoints);
        }

        Move<GoldenEggs>.To<GiantRoom>();

        if (Player.Location is GiantRoom)
        {
            Print("\n\nA large nest full of golden eggs suddenly appears out of nowhere!");
        }

        return true;
    }

    protected bool Count(int i)
    {
        if (feeFieCount != i)
        {
            feeFieCount = 0;
            return Print("Get it right, dummy!");
        }
        else if (feeFieCount++ == 3)
        {
            return Handle();
        }
        else
        {
            Print("Ok.");
        }

        return true;
    }

    public class Fee : FeeFieFoeFoo
    {
        public Fee()
        {
            Verbs = ["fee"];
        }

        public override bool Handler(Object _, Object __)
        {
            return Count(0);
        }
    }

    public class Fie : FeeFieFoeFoo
    {
        public Fie()
        {
            Verbs = ["fie"];
        }

        public override bool Handler(Object _, Object __)
        {
            return Count(1);
        }
    }

    public class Foe : FeeFieFoeFoo
    {
        public Foe()
        {
            Verbs = ["foe"];
        }

        public override bool Handler(Object _, Object __)
        {
            return Count(2);
        }

    }

    public class Foo : FeeFieFoeFoo
    {
        public Foo()
        {
            Verbs = ["foo"];
        }

        public override bool Handler(Object _, Object __)
        {
            return Count(3);
        }
    }
}
