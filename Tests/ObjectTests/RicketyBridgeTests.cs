using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.ObjectTests;

public class RicketyBridgeTests : BaseTestFixture
{
    public RicketyBridgeTests()
    {
        Location = Rooms.Get<SwSideOfChasm>();

        var lamp = Inventory.Add<BrassLantern>();
        Execute("turn on lamp");
        ClearOutput();
    }

    [Fact]
    public void should_suggest_not_jumping()
    {
        Execute("jump");
        Assert.Contains("I respectfully suggest you go across the bridge instead of jumping.", ConsoleOut);
    }
    
    [Fact]
    public void should_jump_and_die()
    {
        var bridge = Objects.Get<RicketyBridge>();
        bridge.Absent = true;

        CommandPrompt.FakeInput("no");

        Execute("jump");
        
        Assert.Contains("You didn't make it.", ConsoleOut);
    }

    [Fact]
    public void troll_should_not_let_you_cross()
    {
        var troll = Objects.Get<BurlyTroll>();
        troll.MoveToLocation();

        Execute("ne");

        Assert.Contains("The troll refuses to let you cross.", ConsoleOut);
    }

    [Fact]
    public void troll_should_jump_out()
    {
        Execute("ne");

        Assert.Contains("The troll steps out from beneath the bridge and blocks your way.", ConsoleOut);
    }

    [Fact]
    public void bridge_should_collapse()
    {
        var troll = Objects.Get<BurlyTroll>();
        var bear = Objects.Get<Bear>();

        troll.HasCaughtTreasure = true;
        bear.IsFollowingYou = true;

        CommandPrompt.FakeInput("no");

        Execute("ne");

        Assert.Contains("as the bridge collapses you stumble back and fall into the chasm", ConsoleOut);
    }

    [Fact]
    public void can_cross_bridge()
    {
        var troll = Objects.Get<BurlyTroll>();

        troll.HasCaughtTreasure = true;

        Execute("ne");

        Assert.Contains("On NE Side of Chasm", ConsoleOut);
    }
}
