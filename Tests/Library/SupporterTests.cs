using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.Library;


public class SupporterTests : BaseTestFixture
{
    [Fact]
    public void should_put_and_take_object_on_surface()
    {
        Execute("take all");
        Execute("turn on lamp");
        Location = Rooms.Get<Y2>();

        var x = ConsoleOut;

        ClearOutput();

        Execute("put lamp on y2");
        Assert.Contains($"You put the brass lantern on the \"Y2\" rock.", ConsoleOut);

        // room should still be lit with lamp sitting on the rock
        Assert.True(CurrentRoom.IsLit());

        ClearOutput();
        Execute("put keys and food on y2");

        ClearOutput();

        // can take lamp from the rock
        Execute("take lamp");

        
        Assert.True(Inventory.Contains(Objects.Get<BrassLantern>()));

    }
}
