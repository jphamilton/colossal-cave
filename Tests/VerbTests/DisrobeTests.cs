using Adventure.Net;
using Xunit;

namespace Tests.VerbTests;

public class DisrobeTests : BaseTestFixture
{

    [Fact]
    public void should_disrobe()
    {
        var cloak = new BlackCloak();
        cloak.Initialize();

        Objects.Add(cloak);

        cloak.MoveToLocation();

        Assert.False(cloak.Worn);

        Execute("wear cloak");

        Assert.Contains("(first taking the black cloak)", ConsoleOut);
        Assert.Contains("You put on the black cloak.", ConsoleOut);

        Assert.True(Inventory.Contains(cloak));

        Assert.True(cloak.Worn);

        ClearOutput();

        Execute("disrobe cloak");

        Assert.Contains("You take off the black cloak.", ConsoleOut);

        Assert.True(Inventory.Contains(cloak));
        Assert.False(cloak.Worn);

    }

    [Fact]
    public void should_take_off_cloak()
    {
        var cloak = new BlackCloak();
        cloak.Initialize();

        Objects.Add(cloak);

        cloak.MoveToLocation();

        Assert.False(cloak.Worn);

        Execute("wear cloak");

        Assert.Contains("(first taking the black cloak)", ConsoleOut);
        Assert.Contains("You put on the black cloak.", ConsoleOut);

        Assert.True(Inventory.Contains(cloak));

        Assert.True(cloak.Worn);

        ClearOutput();

        Execute("take off cloak");

        Assert.Contains("You take off the black cloak.", ConsoleOut);

        Assert.True(Inventory.Contains(cloak));
        Assert.False(cloak.Worn);

    }


    [Fact]
    public void should_implicitly_disrobe()
    {
        var cloak = Objects.Get<BlackCloak>();
        cloak.MoveToLocation();

        Assert.False(cloak.Worn);

        Execute("wear cloak");

        Assert.True(Inventory.Contains(cloak));
        Assert.True(cloak.Worn);

        ClearOutput();

        Execute("disrobe");

        Assert.Contains("(the black cloak)", ConsoleOut);
        Assert.Contains("You take off the black cloak.", ConsoleOut);

        Assert.True(Inventory.Contains(cloak));
        Assert.False(cloak.Worn);

    }

    [Fact]
    public void should_respond_to_partial_disrobe()
    {
        var hat = Objects.Get<BlackHat>();
        Inventory.Add(hat);

        var cloak = Objects.Get<BlackCloak>();
        Inventory.Add(cloak);

        cloak.Worn = true;
        hat.Worn = true;

        ClearOutput();

        CommandPrompt.FakeInput("cloak");

        Execute("disrobe");

        // this is returned by Disrobe, not the parser - need to be able to respond to this
        Assert.Contains("What do you want to disrobe?", ConsoleOut);
        Assert.Contains("You take off the black cloak.", ConsoleOut);
    }


}
