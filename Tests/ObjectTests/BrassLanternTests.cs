using Adventure.Net;
using Adventure.Net.Places;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.ObjectTests;

public class BrassLanternTests : BaseTestFixture
{
    private BrassLantern lamp;

    public BrassLanternTests()
    {
        lamp = Objects.Get<BrassLantern>();
        Inventory.Add(lamp);
    }

    [Fact]
    public void should_accept_fresh_batteries()
    {
        var fresh = Objects.Get<FreshBatteries>();
        Inventory.Add(fresh);

        lamp.PowerRemaining = 0;

        Execute("put batteries in lamp");

        Assert.Equal(2500, lamp.PowerRemaining);

        Assert.Contains("I'm taking the liberty of replacing the batteries.", ConsoleOut);

        var old = Objects.Get<OldBatteries>();

        Assert.True(old.InScope);
        Assert.True(fresh.HaveBeenUsed);
    }

    [Fact]
    public void should_reject_old_batteries()
    {
        Inventory.Add(Objects.Get<OldBatteries>());

        Execute("put batteries in lamp");

        Assert.Contains("Those batteries are dead; they won't do any good at all.", ConsoleOut);
    }

    [Fact]
    public void should_not_accept_anything_other_than_batteries()
    {
        var bottle = Objects.Get<Bottle>();
        Inventory.Add(bottle);

        Execute("put bottle in lamp");

        Assert.Contains("The only thing you might successfully put in the lamp is a fresh pair of batteries.", ConsoleOut);
    }

    [Fact]
    public void should_not_switch_on_if_batteries_are_dead()
    {
        lamp.PowerRemaining = 0;
        lamp.On = false;

        Execute("turn on lamp");

        Assert.False(lamp.On);

        Assert.Contains("Unfortunately, the batteries seem to be dead.", ConsoleOut);
    }

    [Fact]
    public void can_turn_on_lamp()
    {
        lamp.On = false;
        lamp.Light = false;

        Assert.False(lamp.Light);

        Execute("turn on lamp");

        Assert.True(lamp.On);
        Assert.True(lamp.Light);
    }

    [Fact]
    public void can_turn_on_lamp_alernate()
    {
        lamp.On = false;
        lamp.Light = false;

        Assert.False(lamp.Light);

        Execute("switch on lamp");

        Assert.True(lamp.On);
        Assert.True(lamp.Light);
    }

    [Fact]
    public void can_turn_off_lamp()
    {
        lamp.On = true;

        Execute("turn off lamp");

        Assert.False(lamp.On);
        Assert.False(lamp.Light);
    }

    [Fact]
    public void can_turn_off_lamp_alt()
    {
        lamp.On = true;

        Execute("switch off lamp");

        Assert.False(lamp.On);
        Assert.False(lamp.Light);
    }

    [Fact]
    public void should_not_turn_on_lamp_twice()
    {
        lamp.On = true;
        lamp.Light = true;

        Execute("turn on lamp");

        Assert.True(lamp.On);
        Assert.True(lamp.Light);
        Assert.Equal("That's already on.", Line1);
    }

    [Fact]
    public void should_not_turn_off_lamp_twice()
    {
        lamp.On = false;
        lamp.Light = false;

        Execute("turn off lamp");

        Assert.False(lamp.On);
        Assert.False(lamp.Light);
        Assert.Equal("That's already off.", Line1);
    }

    [Fact]
    public void should_display_room_after_turning_on_lamp_in_darkness()
    {
        Location = Rooms.Get<CobbleCrawl>();

        var lamp = Objects.Get<BrassLantern>();
        Inventory.Add(lamp);

        Execute("w");

        Assert.False(CurrentRoom.IsLit());

        ClearOutput();

        Execute("turn on lamp");

        Assert.True(CurrentRoom.IsLit());

        Assert.Contains($"You switch {lamp.DName} on.", ConsoleOut);
        Assert.Contains("You are in a debris room", ConsoleOut);
    }

    [Fact]
    public void room_was_dark_now_its_not()
    {
        Location = Rooms.Get<SwSideOfChasm>();

        Execute("look");
        Assert.Contains("It's pitch dark, and you can't see a thing.", ConsoleOut);

        ClearOutput();

        var lamp = Inventory.Add<BrassLantern>();
        Execute("turn on lamp");

        Assert.Contains("You are on one side of a large, deep chasm.", ConsoleOut);

    }

    [Fact]
    public void room_was_lit_now_its_not()
    {
        Location = Rooms.Get<SwSideOfChasm>();

        Execute("look");
        Assert.Contains("It's pitch dark, and you can't see a thing.", ConsoleOut);

        ClearOutput();

        var lamp = Inventory.Add<BrassLantern>();
        Execute("turn on lamp");

        Assert.Contains("You are on one side of a large, deep chasm.", ConsoleOut);

        ClearOutput();

        Execute("turn off lamp");

        Assert.Contains("It is now pitch dark in here!", ConsoleOut);
    }

    [Fact]
    public void light_is_in_a_transparent_container()
    {
        Location = Rooms.Get<SwSideOfChasm>();

        Execute("look");
        Assert.Contains("It's pitch dark, and you can't see a thing.", ConsoleOut);

        ClearOutput();

        var cage = Inventory.Add<WickerCage>();
        var lamp = Inventory.Add<BrassLantern>();

        var x = Inventory.Items;

        Execute("turn on lamp");

        Assert.Contains("You are on one side of a large, deep chasm.", ConsoleOut);

        ClearOutput();

        Execute("put lamp in cage");
        Execute("close cage");
        Execute("look");

        ClearOutput();

        Assert.True(CurrentRoom.IsLit());
    }

    [Fact]
    public void light_is_in_an_opaque_container()
    {
        Location = Rooms.Get<SwSideOfChasm>();

        Execute("look");
        Assert.Contains("It's pitch dark, and you can't see a thing.", ConsoleOut);

        ClearOutput();

        var box = Inventory.Add<OpaqueBox>();
        Assert.False(box.Transparent);
        Assert.True(box.Open);

        var lamp = Inventory.Add<BrassLantern>();

        // turn on lamp to see room
        Execute("turn on lamp");
        Assert.Contains("You are on one side of a large, deep chasm.", ConsoleOut);
        ClearOutput();

        var x = ConsoleOut;

        Execute("put lamp in box");
        Execute("close box");

        Assert.Contains("It is now pitch dark in here!", ConsoleOut);

        ClearOutput();

        Assert.False(CurrentRoom.IsLit());
    }
}
