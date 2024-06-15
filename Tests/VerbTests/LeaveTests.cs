using Adventure.Net;
using ColossalCave.Places;
using Xunit;

namespace Tests.VerbTests;

public class LeaveTests : BaseTestFixture
{
    [Fact]
    public void can_leave()
    {
        Execute("leave");
        Assert.Equal(Location, Rooms.Get<EndOfRoad>());
    }
}
