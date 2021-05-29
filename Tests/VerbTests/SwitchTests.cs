using Xunit;

namespace Tests.VerbTests
{
    public class SwitchTests : BaseTestFixture
    {
        [Fact]
        public void cannot_switch_on_things_without_switches()
        {
            Execute("switch on keys");
            Assert.Equal("That's not something you can switch.", Line(1));
        }

        [Fact]
        public void cannot_switch_off_things_without_switches()
        {
            var result = Execute("switch off keys");
            Assert.Equal("That's not something you can switch.", Line(1));
        }

    }
}
