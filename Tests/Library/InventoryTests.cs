using Adventure.Net;
using Adventure.Net.ActionRoutines;
using ColossalCave.Actions;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;
using static ColossalCave.Actions.FeeFieFoeFoo;

namespace Tests.Library;

public class InventoryTests : BaseTestFixture
{
    [Fact]
    public void carrying_too_much_should_include_contained_objects()
    {
        Execute("take all");

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bottle = Objects.Get<Bottle>();

        cage.Add(bottle);

        Assert.Equal(5, Inventory.Count);

        Inventory.Add(Objects.Get<EggSizedEmerald>());
        Inventory.Add(Objects.Get<RareCoins>());
        Inventory.Add(Objects.Get<PreciousJewelry>());

        Assert.Equal(8, Inventory.Count);

        var diamonds = Objects.Get<Diamonds>();
        diamonds.MoveToLocation();

        ClearOutput();
        Execute("take diamonds");

        Assert.False(Inventory.Contains(diamonds));

        Assert.Contains("You're carrying too many things already.", ConsoleOut);
    }

    [Fact]
    public void should_display_container_contents()
    {
        var cage = Inventory.Add<WickerCage>();

        Execute("put all in cage");
        ClearOutput();
        Execute("i");

        Assert.Contains("a wicker cage (which is open)", ConsoleOut);
        Assert.Contains("\ta small bottle", ConsoleOut);
        Assert.Contains("\ta brass lantern", ConsoleOut);
        Assert.Contains("\ta set of keys", ConsoleOut);
        Assert.Contains("\tsome tasty food", ConsoleOut);
    }
}
