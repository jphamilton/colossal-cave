using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class ExamineTests : BaseTestFixture
{
    [Fact]
    public void should_examine_scenery()
    {
        Execute("examine stream");
        Assert.Contains($"You see nothing special about the stream.", ConsoleOut);
    }

    [Fact]
    public void should_examine_in_darkness()
    {
        Location = Rooms.Get<DebrisRoom>();
        Assert.False(Location.Light);

        Inventory.Add<SetOfKeys>();

        Execute("examine keys");
        Assert.Contains("Darkness, noun. An absence of light to see by.", ConsoleOut);
    }

    [Fact]
    public void should_examine_room_when_described()
    {
        Execute("examine building");
        Assert.Contains("It's a small brick building. It seems to be a well house.", ConsoleOut);
    }

    [Fact]
    public void should_not_examine_room_not_described()
    {
        Location = Rooms.Get<DeadEndCrawl>();
        var lamp = Inventory.Add<BrassLantern>();
        Execute("turn on lamp");
        ClearOutput();
        Execute("examine dead");
        Assert.Contains("That's not something you need to refer to in the course of this game.", ConsoleOut);
    }
}
