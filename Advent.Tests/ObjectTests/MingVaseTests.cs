using ColossalCave;
using ColossalCave.MyObjects;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.ObjectTests
{
    [TestFixture]
    public class MingVaseTests : AdventTestFixture
    {
        private Treasure vase;

        protected override void OnSetUp()
        {
            Location = Rooms.Get<InsideBuilding>();
            vase = Objects.Get<MingVase>();
            Inventory.Add(vase);
        }

        [Test]
        public void should_break()
        {
            var results = parser.Parse("drop vase");
            results.ShouldContain("The ming vase drops with a delicate crash.");
        }

        [Test]
        public void vase_should_not_exist()
        {
            var results = parser.Parse("drop vase");
            Assert.IsFalse(Inventory.Contains(vase));
            Assert.IsFalse(Location.Contains(vase));
        }

        [Test]
        public void should_not_break()
        {
            var pillow = Objects.Get<VelvetPillow>();
            Location.Objects.Add(pillow);
            var results = parser.Parse("drop vase");
            results.ShouldContain("(coming to rest, delicately, on the velvet pillow)");
            results.ShouldContain("Dropped.");
        }

        [Test]
        public void can_attack_the_vase()
        {
            var results = parser.Parse("hit vase");
            results.ShouldContain("You have taken the vase and hurled it delicately to the ground.");
        }

        [Test]
        public void cannot_fill_vase()
        {
            var bottle = Objects.Get<Bottle>();
            Inventory.Add(bottle);
            var results = parser.Parse("put bottle in vase");
            results.ShouldContain("The vase is too fragile to use as a container.");
        }
    }
}
