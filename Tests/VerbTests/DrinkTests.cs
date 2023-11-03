using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Tests;
using Xunit;

namespace Advent.Tests.Verbs;


public class DrinkTests : BaseTestFixture
{
    [Fact]
    public void cannot_drink_if_there_is_nothing_to_drink()
    {
        Location = Rooms.Get<BelowTheGrate>();
        Execute("drink water");
        Assert.Equal(Messages.CantSeeObject, Line1);
    }

    [Fact]
    public void can_drink_contained_liquids()
    {
        Location = Rooms.Get<BelowTheGrate>();

        Bottle bottle = Objects.Get<Bottle>();

        bottle.Add<WaterInTheBottle>();

        Inventory.Add(bottle);

        Execute("drink h2o");
        Assert.Contains("You drink the cool, refreshing water, draining the bottle in the process.", ConsoleOut);
    }

    [Fact]
    public void should_favor_location_over_inventory()
    {
        // there are two sources of water: the stream and the bottle in inventory
        Location = Rooms.Get<InsideBuilding>();

        Bottle bottle = Objects.Get<Bottle>();
        bottle.Add<WaterInTheBottle>();

        Inventory.Add(bottle);

        Execute("drink water");

        Assert.Contains("You have taken a drink from the stream.", ConsoleOut);
        Assert.Contains("The water tastes strongly of minerals, but is not unpleasant.", ConsoleOut);
        Assert.Contains("It is extremely cold.", ConsoleOut);
    }

    [Fact]
    public void should_empty_bottle()
    {
        Location = Rooms.Get<Forest1>();

        Bottle bottle = Objects.Get<Bottle>();
        bottle.Add<WaterInTheBottle>();

        Inventory.Add(bottle);

        Assert.Single(bottle.Children);

        Execute("drink water");

        Assert.Equal("You drink the cool, refreshing water, draining the bottle in the process.", Line1);
        Assert.Empty(bottle.Children);
    }

    [Fact]
    public void should_drink_from_stream()
    {
        Location = Rooms.Get<InsideBuilding>();

        Execute("drink water");

        Assert.Equal("You have taken a drink from the stream. " +
                        "The water tastes strongly of minerals, but is not unpleasant. " +
                        "It is extremely cold.", Line1);
    }

    [Fact]
    public void cannot_drink_just_anything()
    {
        Location = Rooms.Get<InsideBuilding>();
        Execute("drink food");
        Assert.Equal("You can't drink that.", Line1);
    }

    [Fact]
    public void cannot_drink_items_that_dont_exist()
    {
        Location = Rooms.Get<InsideBuilding>();
        Execute("drink cola");
        Assert.Equal(Messages.CantSeeObject, Line1);
    }
}
