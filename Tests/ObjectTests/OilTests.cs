using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.ObjectTests;

public class OilTests : BaseTestFixture
{
    private readonly PoolOfOil oil;

    public OilTests()
    {
        Context.Story.Location = Room<DebrisRoom>();
        oil = Objects.Get<PoolOfOil>();
        ObjectMap.Add(oil, Location);
    }

    [Fact]
    public void cannot_drink_oil()
    {
        Execute("drink oil");
        Assert.Contains("Absolutely not.", ConsoleOut);
        Assert.True(CurrentRoom.Has<PoolOfOil>());
    }

    [Fact]
    public void cannot_take_oil_without_bottle()
    {
        Execute("take oil");
        Assert.Contains("You have nothing in which to carry the oil.", ConsoleOut);
    }

    [Fact]
    public void can_take_oil()
    {
        var bottle = Objects.Get<Bottle>();
        Inventory.Add(bottle);
        Execute("take oil");
        Assert.Contains("The bottle is now full of oil.", ConsoleOut);
    }

    [Fact]
    public void can_insert_oil()
    {
        var bottle = Objects.Get<Bottle>();
        Inventory.Add(bottle);
        Execute("insert oil into bottle");
        Assert.Contains("The bottle is now full of oil.", ConsoleOut);
    }

    [Fact]
    public void can_fill_with_oil()
    {
        var bottle = Objects.Get<Bottle>();
        Inventory.Add(bottle);
        Execute("fill bottle");
        Assert.Contains("The bottle is now full of oil.", ConsoleOut);
    }

}
