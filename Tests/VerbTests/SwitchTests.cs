using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests
{
    public class SwitchTests : BaseTestFixture
    {
        [Fact]
        public void should_not_switch_on_things_without_switches()
        {
            Execute("switch on keys");
            Assert.Equal("That's not something you can switch.", Line(1));
        }

        [Fact]
        public void should_not_switch_off_things_without_switches()
        {
            Execute("switch off keys");
            Assert.Equal("That's not something you can switch.", Line(1));
        }

        [Fact]
        public void should_implicitly_switch_on_held_item()
        {
            var lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);

            Assert.False(lamp.IsOn);

            Execute("on");

            Assert.Equal($"You switch {lamp} on.", Line(1));
            Assert.True(lamp.IsOn);
        }

        [Fact]
        public void should_implicitly_switch_off_held_item()
        {
            var lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);

            lamp.IsOn = true;

            Execute("off");

            Assert.Equal($"You switch {lamp} off.", Line(1));
            Assert.False(lamp.IsOn);
        }
    }
}
