using System.Diagnostics.CodeAnalysis;

namespace ColossalCave.Places;

[ExcludeFromCodeCoverage]
public abstract class DifferentMaze : BelowGround
{
    public override void Initialize()
    {
        Name = "Maze";
    }
}

public class DifferentMaze1 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a maze of twisty little passages, all different.";
        SouthTo<DifferentMaze3>();
        SouthWestTo<DifferentMaze4>();
        NorthEastTo<DifferentMaze5>();
        SouthEastTo<DifferentMaze6>();
        UpTo<DifferentMaze7>();
        NorthWestTo<DifferentMaze8>();
        EastTo<DifferentMaze9>();
        WestTo<DifferentMaze10>();
        NorthTo<DifferentMaze11>();
        DownTo<WestEndOfLongHall>();

    }
}

public class DifferentMaze2 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a little maze of twisting passages, all different.";
        SouthWestTo<DifferentMaze3>();
        NorthTo<DifferentMaze4>();
        EastTo<DifferentMaze5>();
        NorthWestTo<DifferentMaze6>();
        SouthEastTo<DifferentMaze7>();
        NorthEastTo<DifferentMaze8>();
        WestTo<DifferentMaze9>();
        DownTo<DifferentMaze10>();
        UpTo<DifferentMaze11>();
        SouthTo<DeadEnd14>();
    }
}

public class DifferentMaze3 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a maze of twisting little passages, all different.";
        WestTo<DifferentMaze1>();
        SouthEastTo<DifferentMaze4>();
        NorthWestTo<DifferentMaze5>();
        SouthWestTo<DifferentMaze6>();
        NorthEastTo<DifferentMaze7>();
        UpTo<DifferentMaze8>();
        DownTo<DifferentMaze9>();
        NorthTo<DifferentMaze10>();
        SouthTo<DifferentMaze11>();
        EastTo<DifferentMaze2>();
    }
}

public class DifferentMaze4 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a little maze of twisty passages, all different.";
        NorthWestTo<DifferentMaze1>();
        UpTo<DifferentMaze3>();
        NorthTo<DifferentMaze5>();
        SouthTo<DifferentMaze6>();
        WestTo<DifferentMaze7>();
        SouthWestTo<DifferentMaze8>();
        NorthEastTo<DifferentMaze9>();
        EastTo<DifferentMaze10>();
        DownTo<DifferentMaze11>();
        SouthEastTo<DifferentMaze2>();

    }
}

public class DifferentMaze5 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a twisting maze of little passages, all different.";
        UpTo<DifferentMaze1>();
        DownTo<DifferentMaze3>();
        WestTo<DifferentMaze4>();
        NorthEastTo<DifferentMaze6>();
        SouthWestTo<DifferentMaze7>();
        EastTo<DifferentMaze8>();
        NorthTo<DifferentMaze9>();
        NorthWestTo<DifferentMaze10>();
        SouthEastTo<DifferentMaze11>();
        SouthTo<DifferentMaze2>();
    }
}

public class DifferentMaze6 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a twisting little maze of passages, all different.";
        NorthEastTo<DifferentMaze1>();
        NorthTo<DifferentMaze3>();
        NorthWestTo<DifferentMaze4>();
        SouthEastTo<DifferentMaze5>();
        EastTo<DifferentMaze7>();
        DownTo<DifferentMaze8>();
        SouthTo<DifferentMaze9>();
        UpTo<DifferentMaze10>();
        WestTo<DifferentMaze11>();
        SouthWestTo<DifferentMaze2>();
    }
}

public class DifferentMaze7 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a twisty little maze of passages, all different.";
        NorthTo<DifferentMaze1>();
        SouthEastTo<DifferentMaze3>();
        DownTo<DifferentMaze4>();
        SouthTo<DifferentMaze5>();
        EastTo<DifferentMaze6>();
        WestTo<DifferentMaze8>();
        SouthWestTo<DifferentMaze9>();
        NorthEastTo<DifferentMaze10>();
        NorthWestTo<DifferentMaze11>();
        UpTo<DifferentMaze2>();
    }
}

public class DifferentMaze8 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a twisty maze of little passages, all different.";
        EastTo<DifferentMaze1>();
        WestTo<DifferentMaze3>();
        UpTo<DifferentMaze4>();
        SouthWestTo<DifferentMaze5>();
        DownTo<DifferentMaze6>();
        SouthTo<DifferentMaze7>();
        NorthWestTo<DifferentMaze9>();
        SouthEastTo<DifferentMaze10>();
        NorthEastTo<DifferentMaze11>();
        NorthTo<DifferentMaze2>();
    }
}

public class DifferentMaze9 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a little twisty maze of passages, all different.";
        SouthEastTo<DifferentMaze1>();
        NorthEastTo<DifferentMaze3>();
        SouthTo<DifferentMaze4>();
        DownTo<DifferentMaze5>();
        UpTo<DifferentMaze6>();
        NorthWestTo<DifferentMaze7>();
        NorthTo<DifferentMaze8>();
        SouthWestTo<DifferentMaze10>();
        EastTo<DifferentMaze11>();
        WestTo<DifferentMaze2>();
    }
}

public class DifferentMaze10 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a maze of little twisting passages, all different.";
        DownTo<DifferentMaze1>();
        EastTo<DifferentMaze3>();
        NorthEastTo<DifferentMaze4>();
        UpTo<DifferentMaze5>();
        WestTo<DifferentMaze6>();
        NorthTo<DifferentMaze7>();
        SouthTo<DifferentMaze8>();
        SouthEastTo<DifferentMaze9>();
        SouthWestTo<DifferentMaze11>();
        NorthWestTo<DifferentMaze2>();
    }
}

public class DifferentMaze11 : DifferentMaze
{
    public override void Initialize()
    {
        Description = "You are in a maze of little twisty passages, all different.";
        SouthWestTo<DifferentMaze1>();
        NorthWestTo<DifferentMaze3>();
        EastTo<DifferentMaze4>();
        WestTo<DifferentMaze5>();
        NorthTo<DifferentMaze6>();
        DownTo<DifferentMaze7>();
        SouthEastTo<DifferentMaze8>();
        UpTo<DifferentMaze9>();
        SouthTo<DifferentMaze10>();
        NorthEastTo<DifferentMaze2>();
    }
}
