using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.Library;

public class InventoryTests : BaseTestFixture
{
    [Fact]
    public void should_include_contained_objects()
    {
        Execute("take all");

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bottle = Objects.Get<Bottle>();

        cage.Add(bottle);

        Assert.Equal(5, Inventory.Count);

        Inventory.Add(Objects.Get<EggSizedEmerald>());
        Inventory.Add(Objects.Get<RareCoins>());

        Assert.Equal(7, Inventory.Count);

        var diamonds = Objects.Get<Diamonds>();
        diamonds.MoveToLocation();

        ClearOutput();
        Execute("take diamonds");

        Assert.False(Inventory.Contains(diamonds));

        Assert.Contains("You're carrying too many things already.", ConsoleOut);
    }
}
