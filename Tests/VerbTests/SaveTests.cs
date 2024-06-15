using Adventure.Net;
using ColossalCave;
using ColossalCave.Places;
using ColossalCave.Things;
using System.IO;
using System.Linq;
using Xunit;

namespace Tests.VerbTests;

public class SaveTests : BaseTestFixture
{
    [Fact]
    public void can_save_game()
    {
        Execute("save testgame");
        var dir = Path.GetDirectoryName(Context.Story.GetType().Assembly.Location);
        var path = Path.Combine(dir, "testgame.sav");
        Assert.True(File.Exists(path));
        Assert.Contains("Ok.", ConsoleOut);
        File.Delete(path);
    }

    [Fact]
    public void should_require_file_name()
    {
        Execute("save");
        Assert.Contains("A file name is required.", ConsoleOut);
    }

    [Fact]
    public void can_restore_game()
    {
        Context.Story.CurrentScore = 200;

        var cobble = Rooms.Get<CobbleCrawl>();
        Location = cobble;

        var lamp = Object.Get<BrassLantern>();
        Inventory.Add(lamp);

        lamp.On = true;

        Inventory.Add(lamp);

        var dir = Path.GetDirectoryName(Context.Story.GetType().Assembly.Location);
        var path = Path.Combine(dir, "testgame.sav");

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        Execute("save testgame");

        Assert.True(File.Exists(path));
        Assert.Contains("Ok.", ConsoleOut);

        CommandPrompt.FakeInput("yes");

        Execute("restart");

        // This is printed directly to console and will not be in command output
        // "Are you sure you want to restart?"
        ClearOutput();

        lamp = Object.Get<BrassLantern>();

        Assert.NotEqual(200, Context.Story.CurrentScore);
        Assert.Equal(Room<EndOfRoad>(), Location);
        Assert.Empty(Inventory.Items);
        Assert.False(lamp.On);

        ClearOutput();

        Execute("restore testgame");

        Assert.Equal(200, Context.Story.CurrentScore);
        Assert.True(Location is CobbleCrawl);
        Assert.Contains(lamp, Inventory.Items);
        Assert.True(lamp.On);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void global_state_is_saving_properly()
    {
        var state = Objects.Get<State>();

        state.CavesClosed = true;
        state.CanyonFrom = Rooms.Get<NSCanyon>();
        state.TreasuresFound = 15;

        Execute("save test_global_state");

        state.CavesClosed = false;
        state.CanyonFrom = null;
        state.TreasuresFound = 0;

        Execute("restore test_global_state");

        state = Objects.Get<State>();

        Assert.True(state.CavesClosed);
        Assert.Equal(state.CanyonFrom, Rooms.Get<NSCanyon>());
        Assert.Equal(15, state.TreasuresFound);
    }
}
