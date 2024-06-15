using Adventure.Net;
using ColossalCave;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.ObjectTests;

public class BlackRodTests : BaseTestFixture
{
    public BlackRodTests()
    {
        var lamp = Inventory.Add<BrassLantern>();
        Execute("turn on lamp");
        ClearOutput();
    }

    [Fact]
    public void when_caves_closed_rod_doesnt_work()
    {
        Global.State.CavesClosed = true;
        Location = Rooms.Get<WestSideOfFissure>();
        var rod = Inventory.Add<BlackRod>();
        Execute("wave rod");
        Assert.Contains("Peculiar. Nothing happens.", ConsoleOut);
        Global.State.CavesClosed = false;
    }

    [Fact]
    public void bridge_should_appear()
    {
        Location = Rooms.Get<WestSideOfFissure>();
        var rod = Inventory.Add<BlackRod>();
        var bridge = Objects.Get<CrystalBridge>();
        
        Assert.True(bridge.Absent);

        Execute("e");
        Assert.Contains("The fissure is too wide.", ConsoleOut);

        ClearOutput();

        Execute("wave rod");
        
        Assert.Contains("A crystal bridge now spans the fissure.", ConsoleOut);
        Assert.False(bridge.Absent);

        ClearOutput();

        Execute("e");
        Assert.Equal(Location, Rooms.Get<EastBankOfFissure>());
    }

    [Fact]
    public void bridge_should_vanish()
    {
        Location = Rooms.Get<WestSideOfFissure>();
        var rod = Inventory.Add<BlackRod>();
        var bridge = Objects.Get<CrystalBridge>();

        Assert.True(bridge.Absent);
        Execute("wave rod");
        Assert.False(bridge.Absent);
        ClearOutput();

        Execute("wave rod");
        Assert.Contains("The crystal bridge has vanished!", ConsoleOut);
        Assert.True(bridge.Absent);
    }

}
