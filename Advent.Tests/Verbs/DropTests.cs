using System.Collections.Generic;
using ColossalCave.MyObjects;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class DropTests : AdventTestFixture
    {
        protected override void OnSetUp()
        {
            Location = Rooms.Get<InsideBuilding>();
        }

        [Test]
        public void can_drop_object_that_is_carried()
        {
            TakeBottle();
            IList<string> results = parser.Parse("drop bottle");
            Assert.IsFalse(Inventory.Contains("bottle"));
            Assert.AreEqual("Dropped.", results[0]);
        }

        [Test]
        public void dropping_object_in_room_but_not_held()
        {
            var bottle = Objects.Get<Bottle>();
            Location.Objects.Add(bottle);
            Assert.IsFalse(Inventory.Contains(bottle));
            IList<string> results = parser.Parse("drop bottle");
            Assert.AreEqual("The small bottle is already here.", results[0]);
        }

        [Test]
        public void cannot_drop_something_not_in_scope()
        {
            Assert.IsFalse(Inventory.Contains("cage"));
            IList<string> results = parser.Parse("drop cage");
            Assert.AreEqual("You can't see any such thing.", results[0]);
        }

        [Test]
        public void can_drop_everything()
        {
            TakeAll();
            IList<string> results = parser.Parse("drop all");
            Assert.IsFalse(Inventory.Contains("bottle"));
            Assert.IsFalse(Inventory.Contains("food"));
            Assert.IsFalse(Inventory.Contains("keys"));
            Assert.IsFalse(Inventory.Contains("lamp"));

            Assert.AreEqual("set of keys: Dropped.", results[0]);
            Assert.AreEqual("tasty food: Dropped.", results[1]);
            Assert.AreEqual("brass lantern: Dropped.", results[2]);
            Assert.AreEqual("small bottle: Dropped.", results[3]);
        }

        [Test]
        public void drop_all_except_object_not_specified()
        {
            var results = parser.Parse("drop all except");
            Assert.AreEqual("What do you want to drop?", results[0]);
        }

        [Test]
        public void drop_all_when_inventory_is_empty()
        {
            var results = parser.Parse("drop all");
            Assert.AreEqual("What do you want to drop those things in?", results[0]);
        }

        [Test]
        public void drop_except_all_is_invalid_order()
        {
            var results = parser.Parse("drop except all");
            Assert.AreEqual("You can't see any such thing.", results[0]);
        }

        [Test]
        public void can_drop_everything_except_bottle()
        {
            TakeAll();
            IList<string> results = parser.Parse("drop everything except bottle");
            Assert.IsTrue(Inventory.Contains("bottle"));
            Assert.IsFalse(Inventory.Contains("food"));
            Assert.IsFalse(Inventory.Contains("keys"));
            Assert.IsFalse(Inventory.Contains("lamp"));

            Assert.AreEqual("set of keys: Dropped.", results[0]);
            Assert.AreEqual("tasty food: Dropped.", results[1]);
            Assert.AreEqual("brass lantern: Dropped.", results[2]);
        }

        [Test]
        public void drop_all_when_holding_nothing()
        {
            Assert.IsTrue(Inventory.Items.Count == 0, "Inventory is not empty!");
            var results = parser.Parse("drop all");
            Assert.AreEqual("What do you want to drop those things in?", results[0]);
        }

        private void TakeBottle()
        {
            parser.Parse("take bottle");
            Assert.IsTrue(Inventory.Contains("bottle"));
        }

        private void TakeAll()
        {
            parser.Parse("take all");
            Assert.AreEqual(4, Inventory.Items.Count);
            Assert.IsTrue(Inventory.Contains("bottle"));
            Assert.IsTrue(Inventory.Contains("food"));
            Assert.IsTrue(Inventory.Contains("keys"));
            Assert.IsTrue(Inventory.Contains("lamp"));
        }
    }
}
