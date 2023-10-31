using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;


public class FeeFieFoeFooTests : BaseTestFixture
{
    [Fact]
    public void eggs_should_appear()
    {
        Location = Room<GiantRoom>();
        
        var eggs = Objects.Get<GoldenEggs>();
        eggs.MoveTo<InsideBuilding>();

        Execute("fee");
        Execute("fie");
        Execute("foe");

        ClearOutput();

        Execute("foo");

        Assert.Contains("A large nest full of golden eggs suddenly appears out of nowhere!", ConsoleOut);
    }

    [Fact]
    public void eggs_should_disappear()
    {
        Location = Room<InsideBuilding>();

        var eggs = Objects.Get<GoldenEggs>();
        eggs.MoveTo<InsideBuilding>();

        Execute("fee");
        Execute("fie");
        Execute("foe");

        ClearOutput();

        Execute("foo");

        Assert.Contains("The nest of golden eggs has vanished!", ConsoleOut);
    }
}
