using Adventure.Net;
using Xunit;

namespace Tests.ParserTests;


[Collection("Sequential")]
public class InputTokenizerTests
{
    [Fact]
    public void Test()
    {
        var inputTokenizer = new InputTokenizer();
        var tokens = inputTokenizer.Tokenize("take keys, lamp and bottle");
        Assert.True(tokens.Count == 4);
        Assert.Equal("take", tokens[0]);
        Assert.Equal("keys", tokens[1]);
        Assert.Equal("lamp", tokens[2]);
        Assert.Equal("bottle", tokens[3]);
    }
}
