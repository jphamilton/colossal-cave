using Adventure.Net;

namespace ColossalCave.Places;

public abstract class DeadEnd : BelowGround
{
    public override void Initialize()
    {
        Name = "Dead End";
        Description = "You have reached a dead end.";
        CantGo = "You'll have to go back the way you came.";

    }
}

public class DeadEnd1 : DeadEnd
{
    public override void Initialize()
    {
        base.Initialize();

        WestTo<AlikeMaze4>();
        OutTo<AlikeMaze4>();
    }
}

public class DeadEnd2 : DeadEnd
{
    public override void Initialize()
    {
        base.Initialize();

        WestTo<AlikeMaze4>();
        OutTo<AlikeMaze4>();
    }
}

public class DeadEnd3 : DeadEnd
{
    public override void Initialize()
    {
        base.Initialize();

        UpTo<AlikeMaze3>();
        OutTo<AlikeMaze3>();
    }
}

public class DeadEnd4 : DeadEnd
{
    public override void Initialize()
    {
        base.Initialize();

        WestTo<AlikeMaze9>();
        OutTo<AlikeMaze9>();
    }
}

public class DeadEnd5 : DeadEnd
{
    public override void Initialize()
    {
        base.Initialize();

        UpTo<AlikeMaze10>();
        OutTo<AlikeMaze10>();
    }
}

public class DeadEnd6 : DeadEnd
{
    public override void Initialize()
    {
        base.Initialize();

        EastTo<BrinkOfPit>();
        OutTo<BrinkOfPit>();
    }
}

public class DeadEnd7 : DeadEnd
{
    public override void Initialize()
    {
        base.Initialize();

        SouthTo<CrossOver>();
        OutTo<CrossOver>();
    }
}

public class DeadEnd8 : DeadEnd
{
    public override void Initialize()
    {
        Description = "The canyon runs into a mass of boulders -- dead end.";

        SouthTo<TallEWCanyon>();
        OutTo<TallEWCanyon>();
    }
}

public class DeadEnd9 : DeadEnd
{
    public override void Initialize()
    {
        WestTo<AlikeMaze11>();
        OutTo<AlikeMaze11>();
    }
}

public class DeadEnd10 : DeadEnd
{
    public override void Initialize()
    {
        SouthTo<AlikeMaze3>();
        OutTo<AlikeMaze3>();
    }
}

public class DeadEnd11 : DeadEnd
{
    public override void Initialize()
    {
        EastTo<AlikeMaze12>();
        OutTo<AlikeMaze12>();
    }
}

public class DeadEnd12 : DeadEnd
{
    public override void Initialize()
    {
        base.Initialize();

        UpTo<AlikeMaze8>();
        OutTo<AlikeMaze8>();
    }
}

public abstract class MazeRoom : BelowGround
{
    public override void Initialize()
    {
        Name = "Maze";
        Description = "You are in a maze of twisty little passages, all alike.";

        OutTo(() =>
        {
            Output.Print("Easier said than done.");
            return this;
        });
    }
}

public class AlikeMaze1 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        UpTo<WestEndOfHallOfMists>();
        NorthTo<AlikeMaze1>();
        EastTo<AlikeMaze2>();
        SouthTo<AlikeMaze4>();
        WestTo<AlikeMaze11>();
    }
}

public class AlikeMaze2 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        WestTo<AlikeMaze1>();
        SouthTo<AlikeMaze3>();
        EastTo<AlikeMaze4>();
    }
}

public class AlikeMaze3 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        EastTo<AlikeMaze2>();
        DownTo<DeadEnd3>();
        SouthTo<AlikeMaze6>();
        NorthTo<DeadEnd13>();
    }
}

public class AlikeMaze4 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        WestTo<AlikeMaze1>();
        NorthTo<AlikeMaze2>();
        EastTo<DeadEnd1>();
        SouthTo<DeadEnd2>();
        UpTo<AlikeMaze14>();
        DownTo<AlikeMaze14>();
    }
}

public class AlikeMaze5 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        EastTo<AlikeMaze6>();
        WestTo<AlikeMaze7>();
    }
}

public class AlikeMaze6 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        EastTo<AlikeMaze3>();
        WestTo<AlikeMaze5>();
        DownTo<AlikeMaze7>();
        SouthTo<AlikeMaze8>();
    }
}

public class AlikeMaze7 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        WestTo<AlikeMaze5>();
        UpTo<AlikeMaze6>();
        EastTo<AlikeMaze8>();
        SouthTo<AlikeMaze9>();
    }
}

public class AlikeMaze8 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        WestTo<AlikeMaze6>();
        EastTo<AlikeMaze7>();
        SouthTo<AlikeMaze8>();
        UpTo<AlikeMaze9>();
        NorthTo<AlikeMaze10>();
        DownTo<DeadEnd12>();
    }
}

public class AlikeMaze9 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        WestTo<AlikeMaze7>();
        NorthTo<AlikeMaze8>();
        SouthTo<DeadEnd4>();
    }
}

public class AlikeMaze10 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        WestTo<AlikeMaze8>();
        NorthTo<AlikeMaze10>();
        DownTo<DeadEnd5>();
        EastTo<BrinkOfPit>();
    }
}

public class AlikeMaze11 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        NorthTo<AlikeMaze1>();
        WestTo<AlikeMaze11>();
        SouthTo<AlikeMaze11>();
        EastTo<DeadEnd9>();
        NorthEastTo<DeadEnd10>();
    }
}

public class AlikeMaze12 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        SouthTo<BrinkOfPit>();
        EastTo<AlikeMaze13>();
        WestTo<DeadEnd11>();
    }
}

public class AlikeMaze13 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        NorthTo<BrinkOfPit>();
        WestTo<AlikeMaze12>();
        NorthWestTo<DeadEnd13>();
    }
}

public class AlikeMaze14 : MazeRoom
{
    public override void Initialize()
    {
        base.Initialize();

        UpTo<AlikeMaze4>();
        OutTo<AlikeMaze4>();
    }
}
