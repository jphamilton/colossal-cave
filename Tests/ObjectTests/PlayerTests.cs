using Xunit;

namespace Tests.ObjectTests;

public class PlayerTests : BaseTestFixture
{
    [Fact]
    public void should_be_as_good_looking_as_ever()
    {
        Execute("look at me");
        Assert.Contains("As good-looking as ever.", ConsoleOut);
    }

    [Fact]
    public void should_be_self_possessed()
    {
        Execute("take me");
        Assert.Contains("You're always self-possessed.", ConsoleOut);
    }
}
