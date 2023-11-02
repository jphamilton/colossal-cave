using Adventure.Net;
using Tests.ObjectTests;
using Xunit;

namespace Tests.ParserTests
{
    public class ImplicitTakeTests : BaseTestFixture
    {
        [Fact]
        public void implicit_take_should_trigger_before_after_routines()
        {
            var oven = new Oven();
            oven.Initialize();

            var rocks = new BagOfRocks();
            rocks.Initialize();

            Objects.Add(oven, CurrentRoom.Location);
            Objects.Add(rocks, CurrentRoom.Location);
            
            Inventory.Add(oven);

            var result = Execute("put rocks in oven");

            Assert.Contains("The bag is too heavy.", ConsoleOut);
            Assert.False(Inventory.Contains(rocks));
        }
    }
}
