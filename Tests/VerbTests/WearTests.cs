using Adventure.Net;
using Xunit;

namespace Tests.VerbTests;


public class WearTests : BaseTestFixture
{
    [Fact]
    public void should_put_on_and_take_off_cloak()
    {
        var cloak = Objects.Get<BlackCloak>();
        cloak.MoveToLocation();

        Execute("put on cloak");

        ClearOutput();

        Assert.True(cloak.Worn);

        Execute("i");

        Assert.Contains("a black cloak (being worn)", ConsoleOut);

        ClearOutput();

        Execute("remove cloak");
        
        ClearOutput();
        Assert.False(cloak.Worn);

        Execute("i");

        Assert.Contains("a black cloak", ConsoleOut);
        Assert.DoesNotContain("a black cloak (being worn)", ConsoleOut);

    }

    [Fact]
    public void should_implicitly_wear_cloak()
    {
        var cloak = Objects.Get<BlackCloak>();
        cloak.MoveToLocation();

        Inventory.Add(cloak);

        Execute("wear");

        Assert.Contains("(the black cloak)", ConsoleOut);
        Assert.Contains("You put on the black cloak.", ConsoleOut);

        Assert.True(cloak.Worn);
    }

    [Fact]
    public void should_respond_to_partial_wear()
    {
        var hat = Objects.Get<BlackHat>();
        hat.MoveToLocation();

        var cloak = Objects.Get<BlackCloak>();
        cloak.MoveToLocation();
        
        Inventory.Add(cloak);
        Inventory.Add(hat);

        CommandPrompt.FakeInput("cloak");
        
        Execute("wear");
        
        // this is returned by Wear, not the parser - need to be able to respond to this
        Assert.Contains("What do you want to wear?", ConsoleOut);
        Assert.Contains("You put on the black cloak.", ConsoleOut);

    }

}
