using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class PourTests : BaseTestFixture
{
    [Fact]
    public void can_pour_water()
    {
        Location = Room<WestPit>();
        var lamp = Inventory.Add<BrassLantern>();
        lamp.On = true;
        Location.Light = true;

        Container bottle = Inventory.Add<Bottle>() as Container;
        bottle.Add<WaterInTheBottle>();

        Execute("pour water on plant");

        Assert.Contains("The plant spurts into furious growth for a few seconds.", ConsoleOut);
        Assert.Contains("There is a 12-foot-tall beanstalk stretching up out of the pit, bellowing \"Water!! Water!!\"", ConsoleOut);
    }

    [Fact]
    public void can_implicit_pour_water()
    {
        Location = Room<WestPit>();
        var lamp = Inventory.Add<BrassLantern>();
        lamp.On = true;
        Location.Light = true;

        Container bottle = Inventory.Add<Bottle>() as Container;
        bottle.Add<WaterInTheBottle>();

        Execute("pour water");

        Assert.Contains($"(on the plant)", ConsoleOut); // this has (the plant) instead of "on the plant"
        Assert.Contains("The plant spurts into furious growth for a few seconds.", ConsoleOut);
        Assert.Contains("There is a 12-foot-tall beanstalk stretching up out of the pit, bellowing \"Water!! Water!!\"", ConsoleOut);
    }

    [Fact]
    public void can_implicit_pour_water_2()
    {
        Location = Room<ImmenseNSPassage>();
        var lamp = Inventory.Add<BrassLantern>();
        lamp.On = true;
        Location.Light = true;

        Container bottle = Inventory.Add<Bottle>() as Container;
        bottle.Add<WaterInTheBottle>();

        Execute("pour water");

        Assert.Contains($"(on the rusty door)", ConsoleOut);
        Assert.Contains("The hinges are quite thoroughly rusted now and won't budge.", ConsoleOut);
    }

    [Fact]
    public void can_pour_oil()
    {
        Location = Room<WestPit>();
        var lamp = Inventory.Add<BrassLantern>();
        lamp.On = true;
        Location.Light = true;

        Container bottle = Inventory.Add<Bottle>() as Container;
        bottle.Add<OilInTheBottle>();

        Execute("pour oil on plant");

        Assert.Contains("The plant indignantly shakes the oil off its leaves and asks, \"Water?\"", ConsoleOut);
    }
}
