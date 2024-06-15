using Adventure.Net;
using Xunit;

namespace Tests.VerbTests;

public class LookTests : BaseTestFixture
{
    [Fact]
    public void can_look()
    {
        Execute("look");
        
        Assert.Contains("Inside Building", ConsoleOut);
        Assert.Contains("You are inside a building, a well house for a large spring.", ConsoleOut);
        Assert.Contains("There are some keys on the ground here.", ConsoleOut);
        Assert.Contains("There is tasty food here.", ConsoleOut);
        Assert.Contains("There is a shiny brass lamp nearby.", ConsoleOut);
        Assert.Contains("There is an empty bottle here.", ConsoleOut);
    }

    [Fact]
    public void can_look_direction()
    {
        Execute("look west");
        Assert.Contains("You see nothing unexpected in that direction.", ConsoleOut);
    }

    [Fact]
    public void can_look_at_something()
    {
        Execute("look at keys");
        Assert.Contains("It's just a normal-looking set of keys.", ConsoleOut);
    }

    [Fact]
    public void can_look_under_something()
    {
        Execute("look under lamp");
        Assert.Contains("You find nothing of interest.", ConsoleOut);
    }
}
