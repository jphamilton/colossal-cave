using Adventure.Net;
using ColossalCave.Places;
using Xunit;

namespace Tests.VerbTests;

public class JumpTests : BaseTestFixture
{
    [Fact]
    public void before_jump_handled()
    {
        Location = Room<EastBankOfFissure>();
        CommandPrompt.FakeInput("n");
        Execute("jump");
        Assert.Contains("You didn't make it.", ConsoleOut);
    }
}
