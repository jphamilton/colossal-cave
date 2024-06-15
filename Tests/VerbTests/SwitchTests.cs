using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class SwitchTests : BaseTestFixture
{
    [Fact]
    public void should_not_switch_on_things_without_switches()
    {
        Execute("switch on keys");
        Assert.Equal("That's not something you can switch on.", Line1);
    }

    [Fact]
    public void should_not_switch_off_things_without_switches()
    {
        Execute("switch off keys");
        Assert.Equal("That's not something you can switch off.", Line1);
    }

    [Fact]
    public void should_implicitly_switch_on_held_item()
    {
        var lamp = Objects.Get<BrassLantern>();
        Inventory.Add(lamp);

        Assert.False(lamp.On);

        Execute("on");

        Assert.Contains($"You switch {lamp.DName} on.", ConsoleOut);
        Assert.True(lamp.On);
    }

    [Fact]
    public void should_implicitly_switch_off_held_item()
    {
        var lamp = Objects.Get<BrassLantern>();
        Inventory.Add(lamp);

        lamp.On = true;

        Execute("off");

        Assert.Contains($"You switch {lamp.DName} off.", ConsoleOut);
        Assert.False(lamp.On);
    }
}
