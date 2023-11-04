using Adventure.Net;
using Xunit;

namespace Tests.VerbTests;


public class WearTests : BaseTestFixture
{
    [Fact]
    public void should_put_on_and_take_off_cloak()
    {
        var cloak = new BlackCloak();
        cloak.Initialize();

        Objects.Add(cloak);

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
}
