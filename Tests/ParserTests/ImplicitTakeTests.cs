using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
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

        [Fact]
        public void should_implicitly_take_indirect_object()
        {
            Location = Rooms.Get<OutsideGrate>();

            // keys are on the ground
            var keys = Objects.Get<SetOfKeys>();
            keys.MoveToLocation();

            var parser = new Parser();

            // key should be implicitly taken
            var result = parser.Parse("unlock grate with key");

            Assert.Equal(keys, result.ImplicitTake);
        }
    }
}
