using Adventure.Net;
using ColossalCave.Things;
using ColossalCave.Places;
using Xunit;

namespace Tests.Verbs
{
    public class EmptyTests : BaseTestFixture
    {
        [Fact]
        public void cannot_empty_things_that_arent_containers()
        {
            Location = Rooms.Get<InsideBuilding>();
            Execute("empty food");
            Assert.Equal("The tasty food can't contain things.", Line1);
        }

        [Fact]
        public void cannot_empty_something_that_is_already_empty()
        {
            Location = Rooms.Get<InsideBuilding>();
            Execute("empty bottle");
            Assert.Equal("The bottle is already empty!", Line1);
        }

        [Fact]
        public void cannot_empty_something_that_is_closed()
        {
            var cage = Objects.Get<WickerCage>() as Container;
            cage.Add<Bottle>();
            cage.Open = false;
            Inventory.Add(cage);

            Execute("empty cage");
            Assert.Equal("The wicker cage is closed.", Line1);
        }
    }
}