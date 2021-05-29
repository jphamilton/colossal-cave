using Adventure.Net;
using ColossalCave.Objects;
using ColossalCave.Places;
using Xunit;

namespace Tests.ObjectTests
{

    public class StreamTests : BaseTestFixture
    {
        public StreamTests()
        {
            Location = Room<InsideBuilding>();
        }

        [Fact]
        public void should_handle_BeforeTake()
        {
            Container bottle = Objects.Get<Bottle>();
            var water = Objects.Get<WaterInTheBottle>();
            Inventory.Add(bottle);

            Execute("take stream");
            Assert.Equal("The bottle is now full of water.", Line(1));

            Assert.True(Inventory.Contains(bottle));
            Assert.True(bottle.Contents.Contains(water));

        }


    }
}
