using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.ObjectTests;

public class PersianRugTests : BaseTestFixture
{
    [Fact]
    public void should_describe_dragon()
    {
        Location = Rooms.Get<SecretCanyon>();
        var lamp = Inventory.Add<BrassLantern>();
        lamp.On = true;
        lamp.Light = true;

        Execute("look");

        Assert.Contains($"The dragon is sprawled out on the Persian rug!", ConsoleOut);
    }
}
