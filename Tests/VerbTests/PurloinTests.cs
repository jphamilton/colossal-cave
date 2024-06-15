using Adventure.Net;
using Xunit;

namespace Tests.VerbTests;

public class PurloinTests : BaseTestFixture
{
    [Fact]
    public void should_pick_correct_object()
    {
        CommandPrompt.FakeInput("rusty mark");

        Execute("purloin rod");
        
        Assert.Contains($"Which do you mean, the black rod with a rusty star on the end or the black rod with a rusty mark on the end?", ConsoleOut);
        Assert.Contains("[Purloined.]", ConsoleOut);
    }
}
