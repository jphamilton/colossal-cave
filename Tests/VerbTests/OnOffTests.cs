using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class OnOffTests : BaseTestFixture
{
    public OnOffTests()
    {
        Location = Rooms.Get<DebrisRoom>();
    }

    [Fact]
    public void on_should_switch_on_lamp()
    {
        var lamp = Inventory.Add<BrassLantern>();
        Execute("on");
        Assert.Contains($"You switch {lamp.DName} on.", ConsoleOut);
        Assert.True(lamp.On);
        Assert.True(CurrentRoom.IsLit());
    }

    [Fact]
    public void on_should_not_switch_on_lamp()
    {
        Execute("on");
        Assert.Contains("You have no lamp", ConsoleOut);
        Assert.False(CurrentRoom.IsLit());
    }

    [Fact]
    public void off_should_switch_off_lamp()
    {
        var lamp = Inventory.Add<BrassLantern>();

        Execute("turn on lamp");
        ClearOutput();

        Execute("off");
        Assert.Contains($"You switch {lamp.DName} off.", ConsoleOut);
        Assert.False(lamp.On);
        Assert.False(CurrentRoom.IsLit());
    }

    [Fact]
    public void off_should_not_switch_off_lamp()
    {
        Location = Rooms.Get<EndOfRoad>();
        Execute("off");
        Assert.Contains("You have no lamp", ConsoleOut);
    }

    [Fact]
    public void should_display_room_after_implicitly_turning_on_lamp_in_darkness()
    {
        Location = Rooms.Get<CobbleCrawl>();

        var lamp = Objects.Get<BrassLantern>();
        Inventory.Add(lamp);

        Execute("w");

        Assert.False(CurrentRoom.IsLit());

        ClearOutput();

        Execute("on"); // implicitly turning on the lamp does not display the room - THIS IS NOT RUNNING BEFORE/AFTER

        Assert.True(CurrentRoom.IsLit());

        Assert.Contains($"You switch {lamp.DName} on.", ConsoleOut);
        Assert.Contains("You are in a debris room", ConsoleOut);
    }
}
