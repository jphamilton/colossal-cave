using Adventure.Net.ActionRoutines;
using Adventure.Net;
using Xunit;
using ColossalCave.Places;

namespace Tests.ParserTests;


public class DirectionalTests : BaseTestFixture
{
    // just need to test all the cases, not every possible direction
    [Fact]
    public void should_enter()
    {
        Location = Rooms.Get<EndOfRoad>();
        var building = Rooms.Get<InsideBuilding>();

        Execute("enter building");
        Assert.Contains(building.Description, ConsoleOut);
        Assert.Equal(building, Location);
    }

    [Fact]
    public void should_enter_2()
    {
        Location = Rooms.Get<EndOfRoad>();
        var building = Rooms.Get<InsideBuilding>();

        Execute("go inside building");
        Assert.Contains(building.Description, ConsoleOut);
        Assert.Equal(building, Location);
    }

    [Fact]
    public void should_replace_verb_with_directional()
    {
        var road = Rooms.Get<EndOfRoad>();

        var parser = new Parser();
        var result = parser.Parse("go west");
        Assert.Equal(Routines.Get<West>(), result.Routine);

        var command = new Command(result);
        command.Run();

        Assert.Equal(road, Location);
    }

    [Fact]
    public void should_exit()
    {
        var road = Rooms.Get<EndOfRoad>();

        var parser = new Parser();
        var result = parser.Parse("out");
        Assert.Equal(Routines.Get<Out>(), result.Routine);

        var command = new Command(result);
        command.Run();

        Assert.Equal(road, Location);
    }

    [Fact]
    public void should_go_where()
    {
        Execute("go");
        Assert.Contains("You'll have to say which compass direction to go in.", ConsoleOut);
    }
}
