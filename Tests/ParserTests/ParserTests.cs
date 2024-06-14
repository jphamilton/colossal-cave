using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.ParserTests;
public class ParserTests : BaseTestFixture
{
    [Fact]
    public void should_handle_bad_verbs()
    {
        var parser = new Parser();
        var result = parser.Parse("alskjlkja");
        Assert.Equal(Messages.VerbNotRecognized, result.Error);
    }
}
