using Adventure.Net;
using Adventure.Net.Things;
using ColossalCave.Places;
using Xunit;

namespace Tests.VerbTests;

public class GoNearTests : BaseTestFixture
{
    [Fact]
    public void should_go_to_room()
    {
        var room = Rooms.Get<BreathtakingView>();
        Execute("gonear breathtaking");
        Assert.Equal(room, Player.Location);
    }

    [Fact]
    public void should_go_to_room_where_object_is_located()
    {
        var room = Rooms.Get<ShellRoom>();
        Execute("gonear clam");
        Assert.Equal(room, Player.Location);
    }
}
