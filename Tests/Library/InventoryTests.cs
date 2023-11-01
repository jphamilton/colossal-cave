using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.Library
{
    public class InventoryTests : BaseTestFixture
    {
        [Fact]
        public void should_include_contained_objects()
        {
            Execute("take all");

            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bottle = Objects.Get<Bottle>();
            
            cage.Add(bottle);

            var items = Inventory.Items;

            Assert.Equal(5, Inventory.Count);

        }
    }
}
