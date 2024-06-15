using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class FillTests : BaseTestFixture
{
    [Fact]
    public void nothing_to_fill()
    {
        var lamp = Inv<BrassLantern>();
        Execute("fill lamp");
        Assert.Contains($"There isn't anything obvious with which to fill {lamp.DName}.", ConsoleOut);
    }

    [Fact]
    public void still_nothing_to_fill()
    {
        var lamp = Get<BrassLantern>();
        var bottle = Get<Bottle>();
        
        Execute("fill lamp from bottle");
        
        Assert.Contains($"Filling {lamp.DName} from {bottle.DName} wouldn't make much sense.", ConsoleOut);
    }
}
