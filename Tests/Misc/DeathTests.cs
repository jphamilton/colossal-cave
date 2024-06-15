using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.Misc;

public class DeathTests : BaseTestFixture
{
    [Fact]
    public void should_leave_items_at_place_of_death()
    {
        var debrisRoom = Rooms.Get<DebrisRoom>();
        
        Location = Rooms.Get<CobbleCrawl>();

        var lamp = Inventory.Add<BrassLantern>();
        var keys = Inventory.Add<SetOfKeys>();
        var food = Inventory.Add<TastyFood>();

        Execute("w"); // into "darkness" but really debris room

        CommandPrompt.FakeInput("y"); // to reincarnate

        Execute("avada kedavra");

        Assert.Equal(lamp.Parent, Rooms.Get<EndOfRoad>());
        Assert.DoesNotContain(keys, Inventory.Items);
        Assert.DoesNotContain(food, Inventory.Items);

        Assert.Equal(keys.Location, debrisRoom);
        Assert.Equal(food.Location, debrisRoom);
    }

    // Blast
    // FissureRoom
    // NarrowCorridor
    // NeEnd
    // SideOfChasm
    // TopOfSmallPit
    // WindowOnPit2

}
