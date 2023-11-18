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
    public void should_implicitly_disrobe()
    {
        var cloak = new BlackCloak();
        cloak.Initialize();

        Objects.Add(cloak);

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
        var hat = new BlackHat();
        hat.Initialize();

        var cloak = new BlackCloak();
        cloak.Initialize();

        cloak.Worn = true;
        hat.Worn = true;

        Objects.Add(cloak);
        Objects.Add(hat);

        Inventory.Add(cloak);
        Inventory.Add(hat);

        ClearOutput();

        Execute("disrobe");
        Execute("cloak");

        // this is returned by Disrobe, not the parser - need to be able to respond to this
        Assert.Contains("What do you want to disrobe?", ConsoleOut);
        
        Assert.Contains("You take off the black cloak.", ConsoleOut);

    }


}
