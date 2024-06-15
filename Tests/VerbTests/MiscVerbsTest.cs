using Adventure.Net;
using Adventure.Net.Extensions;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

// adding verbs from Inform 6 and testing them
public class MiscVerbsTest : BaseTestFixture
{
    [Fact]
    public void can_blow()
    {
        var bottle = Objects.Get<Bottle>();
        Execute("blow bottle");
        Assert.Contains($"(first taking {bottle.DName})", ConsoleOut);
        Assert.Contains($"You can't usefully blow {bottle.ThatOrThose}.", ConsoleOut);
    }

    [Fact]
    public void can_burn()
    {
        var bottle = Objects.Get<Bottle>();
        Execute("burn bottle");
        Assert.Contains($"This dangerous act would achieve little.", ConsoleOut);
    }

    [Fact]
    public void can_burn_with()
    {
        var lamp = Objects.Get<BrassLantern>();
        Execute("burn bottle with lamp");
        Assert.Contains($"(first taking {lamp.DName})", ConsoleOut);
        Assert.Contains($"This dangerous act would achieve little.", ConsoleOut);
    }

    [Fact]
    public void should_turn_on_lamp_instead_of_burning()
    {
        var bottle = Objects.Get<Bottle>();
        Execute("burn lamp with bottle");
        Assert.Contains($"(first taking {bottle.DName})", ConsoleOut);
        Assert.Contains($"This dangerous act would achieve little.", ConsoleOut);
    }

    [Fact]
    public void can_buy()
    {
        var bottle = Objects.Get<Bottle>();
        Execute("buy lamp");
        Assert.Contains("Nothing is on sale.", ConsoleOut);
    }

    [Fact]
    public void can_cut()
    {
        var bottle = Objects.Get<Bottle>();
        Execute("cut lamp");
        Assert.Contains("Cutting that up would achieve little.", ConsoleOut);
    }

    [Fact]
    public void can_dig()
    {
        var bottle = Objects.Get<Bottle>();
        Execute("dig lamp");
        Assert.Contains("Digging would achieve nothing here.", ConsoleOut);
    }

    [Fact]
    public void can_dig_with()
    {
        var bottle = Objects.Get<Bottle>();
        Execute("dig lamp with bottle");
        Assert.Contains("Digging would achieve nothing here.", ConsoleOut);
    }

    [Fact]
    public void can_dig_in()
    {
        var bottle = Objects.Get<Bottle>();
        Execute("dig in stream");
        Assert.Contains("Digging would achieve nothing here.", ConsoleOut);
    }

    [Fact]
    public void can_kiss()
    {
        var bird = Objects.Get<LittleBird>();
        bird.MoveToLocation();

        Execute("kiss bird");

        Assert.Contains("Keep your mind on the game.", ConsoleOut);
    }

    [Fact]
    public void cant_kiss_things()
    {
        Execute("kiss bottle");

        Assert.Contains(Messages.AnimateOnly, ConsoleOut);
    }

    [Fact]
    public void can_listen()
    {
        Execute("listen");
        Assert.Contains("You hear nothing unexpected", ConsoleOut);
        ClearOutput();

        Execute("listen stream");
        Assert.Contains("You hear nothing unexpected", ConsoleOut);
        ClearOutput();

        Execute("listen to bottle");
        Assert.Contains("You hear nothing unexpected", ConsoleOut);
        ClearOutput();

    }
}
