using Adventure.Net;
using Adventure.Net.ActionRoutines;
using ColossalCave.Places;
using Xunit;

namespace Tests.VerbTests;

public class CatchTests : BaseTestFixture
{
    [Fact]
    public void cannot_catch_inanimate_objects()
    {
        Location = Room<InsideBuilding>();
        Execute("catch bottle");
        Assert.Contains(Messages.AnimateOnly, ConsoleOut);
    }

    [Fact]
    public void cant_catch_this()
    {
        Location = Room<InsideBuilding>();

        var shark = Objects.Get<Shark>();
        shark.MoveToLocation();

        Execute("catch shark");
        Assert.Equal("You can't catch that.", Line1);
    }

    [Fact]
    public void can_catch_this()
    {
        Location = Room<InsideBuilding>();

        var octopus = Objects.Get<Octopus>();
        octopus.MoveToLocation();

        Execute("catch octopus");
        Assert.Equal("Yeah right!", Line1);
    }

}

public class Shark : Object
{
    public override void Initialize()
    {
        Name = "shark";
        Synonyms.Are("shark", "great white", "white shark");
        Animate = true;
    }
}

public class Octopus : Object
{
    public override void Initialize()
    {
        Name = "octopus";
        Synonyms.Are("octopus", "octopi");
        Animate = true;

        Before<Catch>(() =>
        {
            Print("Yeah right!");
            return true;
        });
    }
}
