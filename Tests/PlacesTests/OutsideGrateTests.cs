using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.PlacesTests;

public class OutsideGrateTests : BaseTestFixture
{
    public OutsideGrateTests()
    {
        Location = Room<OutsideGrate>();
    }

    [Fact]
    public void should_open_grate_before_going_down()
    {
        var keys = Objects.Get<SetOfKeys>();
        Inventory.Add(keys);

        Execute("unlock grate");

        ClearOutput();

        Execute("down");

        Assert.Equal(Rooms.Get<BelowTheGrate>(), Location);

        // messages are being displayed out of order
        Assert.StartsWith("(first opening the steel grate)", ConsoleOut);

    }
}
