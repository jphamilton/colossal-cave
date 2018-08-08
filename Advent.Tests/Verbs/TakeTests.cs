using System.Collections.Generic;
using ColossalCave.MyObjects;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class TakeTests : AdventTestFixture
    {

        protected override void OnSetUp()
        {
            Location = Rooms.Get<InsideBuilding>();
        }

        [Test]
        public void can_take_one_object()
        {
            Object bottle = Objects.Get<Bottle>();
            
            IList<string> results = parser.Parse("take bottle");

            Assert.AreEqual("Taken.", results[0]);

            Assert.IsTrue(Inventory.Contains(bottle));
            Assert.IsFalse(Location.Objects.Contains(bottle));
        }

        [Test]
        public void cannot_take_something_which_is_not_around()
        {
            Object cage = Objects.Get<WickerCage>();
            IList<string> results = parser.Parse("take cage");

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("You can't see any such thing.", results[0]);

            Assert.IsFalse(Inventory.Contains(cage));
        }

        [Test]
        public void cannot_take_scenic_objects()
        {
            IList<string> results = parser.Parse("take building");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("That's hardly portable.", results[0]);
        }

        [Test]
        public void cannot_take_static_objects()
        {
            Location = Rooms.Get<OutsideGrate>();
            IList<string> results = parser.Parse("take grate");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("That's fixed in place.", results[0]);
        }

        [Test]
        public void can_take_multiple_objects()
        {
            Object bottle = Objects.Get<Bottle>();
            Object keys = Objects.Get<SetOfKeys>();

            IList<string> results = parser.Parse("take bottle and keys");

            Assert.AreEqual("small bottle: Taken.", results[0]);
            Assert.AreEqual("set of keys: Taken.", results[1]);

            Assert.IsTrue(Inventory.Contains(bottle));
            Assert.IsTrue(Inventory.Contains(keys));
        }

        [Test]
        public void can_take_all()
        {
            Object bottle = Objects.Get<Bottle>();
            Object keys = Objects.Get<SetOfKeys>();
            Object food = Objects.Get<TastyFood>();
            Object lamp = Objects.Get<BrassLantern>();

            Location.Objects.Add(bottle);
            Location.Objects.Add(keys);
            Location.Objects.Add(food);
            Location.Objects.Add(lamp);

            var results = parser.Parse("take all");

            Assert.IsTrue(Inventory.Contains(bottle, keys, lamp, food));

            Assert.IsTrue(results.Contains("set of keys: Taken."));
            Assert.IsTrue(results.Contains("tasty food: Taken."));
            Assert.IsTrue(results.Contains("brass lantern: Taken."));
            Assert.IsTrue(results.Contains("small bottle: Taken."));
            Assert.IsTrue(results.Contains("spring: That's hardly portable."));
            Assert.IsTrue(results.Contains("pair of 1 foot diameter sewer pipes: That's hardly portable."));
            Assert.IsTrue(results.Contains("well house: That's hardly portable."));
            Assert.IsTrue(results.Contains("stream: The bottle is now full of water."));
            
        }

        [Test]
        public void can_take_all_except_object()
        {
            Object bottle = Objects.Get<Bottle>();
            Object keys = Objects.Get<SetOfKeys>();
            Object food = Objects.Get<TastyFood>();
            Object lamp = Objects.Get<BrassLantern>();

            parser.Parse("take all except food");

            Assert.IsTrue(Inventory.Contains(bottle, keys, lamp));
            Assert.IsFalse(Inventory.Contains(food));
        }

        [Test]
        public void cant_take_something_you_already_have()
        {
            Object bottle = Objects.Get<Bottle>();

            Location.Objects.Remove(bottle);
            Inventory.Add(bottle);

            var results = parser.Parse("take bottle");
            
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("You already have that.", results[0]);

        }

        [Test]
        public void take_except()
        {
            var results = parser.Parse("take except");
            Assert.AreEqual(L.CantSeeObject, results[0]);
            
        }

        [Test]
        public void take_bottle_lantern_food_except_bottle()
        {
            var results = parser.Parse("take bottle lantern food except bottle");
            Assert.IsTrue(results.Contains("tasty food: Taken."));
            Assert.IsTrue(results.Contains("brass lantern: Taken."));
        }
    }
}