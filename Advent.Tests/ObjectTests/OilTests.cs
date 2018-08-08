using ColossalCave.MyObjects;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.ObjectTests
{
    [TestFixture]
    public class OilTests : AdventTestFixture
    {
        private Oil oil;

        protected override void OnSetUp()
        {
            Location = Rooms.Get<DebrisRoom>();
            oil = Objects.Get<Oil>();
            Location.Objects.Add(oil);
        }

        protected override void OnTearDown()
        {
            Location.Objects.Remove(oil);
        }
        
        [Test]
        public void cannot_drink_oil()
        {
            var results = parser.Parse("drink oil");
            results.ShouldContain("Absolutely not.");
            Assert.IsTrue(Location.Objects.Contains(oil));
        }

        [Test]
        public void cannot_take_oil_without_bottle()
        {
            var results = parser.Parse("take oil");
            results.ShouldContain("You have nothing in which to carry the oil.");
        }

        [Test]
        public void can_take_oil()
        {
            var bottle = Objects.Get<Bottle>();
            Inventory.Add(bottle);
            var results = parser.Parse("take oil");
            results.ShouldContain("The bottle is now full of oil.");
        }

        [Test]
        public void can_insert_oil()
        {
            var bottle = Objects.Get<Bottle>();
            Inventory.Add(bottle);
            var results = parser.Parse("insert oil into bottle");
            results.ShouldContain("The bottle is now full of oil.");
        }
    }
}
