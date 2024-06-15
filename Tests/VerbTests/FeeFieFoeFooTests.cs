using Adventure.Net;
using ColossalCave;
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

    [Fact]
    public void should_get_it_right_dummy()
    {
        Location = Room<GiantRoom>();

        var eggs = Objects.Get<GoldenEggs>();
        eggs.MoveTo<InsideBuilding>();

        Execute("fee");
        Execute("fee");
        //Execute("foe");

        Assert.Contains("Get it right, dummy!", ConsoleOut);
    }

    [Fact]
    public void eggs_are_already_here()
    {
        Location = Room<GiantRoom>();

        var eggs = Objects.Get<GoldenEggs>();
        eggs.MoveToLocation();

        Execute("fee");
        Execute("fie");
        Execute("foe");

        ClearOutput();

        Execute("foo");

        Assert.Contains("Nothing happens.", ConsoleOut);
    }

    [Fact]
    public void eggs_are_zapped_out_of_inventory()
    {
        Context.Story.CurrentScore = 10;

        Location = Room<GiantRoom>();
        var eggs = Inventory.Add<GoldenEggs>();

        Execute("fee");
        Execute("fie");
        Execute("foe");

        ClearOutput();

        Execute("foo");

        Assert.Contains("The nest of golden eggs has vanished!", ConsoleOut);
        Assert.Contains("A large nest full of golden eggs suddenly appears out of nowhere!", ConsoleOut);
        Assert.Equal(Location, eggs.Location);
        Assert.Equal(5, Context.Story.CurrentScore);
    }
}
