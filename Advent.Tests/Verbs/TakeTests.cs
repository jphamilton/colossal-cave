using System.Collections.Generic;
using ColossalCave.Objects;
using ColossalCave.Places;
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
            Item bottle = Items.Get<Bottle>();
            
            IList<string> results = parser.Parse("take bottle");

            Assert.AreEqual("Taken.", results[0]);

            Assert.IsTrue(Inventory.Contains(bottle));
            Assert.IsFalse(Location.Objects.Contains(bottle));
        }

        [Test]
        public void cannot_take_something_which_is_not_around()
        {
            Item cage = Items.Get<WickerCage>();
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
            Item bottle = Items.Get<Bottle>();
            Item keys = Items.Get<SetOfKeys>();

            IList<string> results = parser.Parse("take bottle and keys");

            Assert.AreEqual("small bottle: Taken.", results[0]);
            Assert.AreEqual("set of keys: Taken.", results[1]);

            Assert.IsTrue(Inventory.Contains(bottle));
            Assert.IsTrue(Inventory.Contains(keys));
        }

        [Test]
        public void can_take_comma_delimited()
        {
            Item bottle = Items.Get<Bottle>();
            Item keys = Items.Get<SetOfKeys>();

            IList<string> results = parser.Parse("take bottle,keys");

            Assert.AreEqual("small bottle: Taken.", results[0]);
            Assert.AreEqual("set of keys: Taken.", results[1]);

            Assert.IsTrue(Inventory.Contains(bottle));
            Assert.IsTrue(Inventory.Contains(keys));
        }

        [Test]
        public void can_take_multiple_objects_using_and()
        {
            Item bottle = Items.Get<Bottle>();
            Item keys = Items.Get<SetOfKeys>();
            Item lantern = Items.Get<BrassLantern>();

            IList<string> results = parser.Parse("take bottle and keys and lantern");

            Assert.AreEqual("small bottle: Taken.", results[0]);
            Assert.AreEqual("set of keys: Taken.", results[1]);
            Assert.AreEqual("brass lantern: Taken.", results[2]);

            Assert.IsTrue(Inventory.Contains(bottle));
            Assert.IsTrue(Inventory.Contains(keys));
            Assert.IsTrue(Inventory.Contains(lantern));
        }

        [Test]
        public void can_take_multiple_objects_using_comma_and()
        {
            Item bottle = Items.Get<Bottle>();
            Item keys = Items.Get<SetOfKeys>();
            Item lantern = Items.Get<BrassLantern>();

            IList<string> results = parser.Parse("take bottle, keys and lantern");

            Assert.AreEqual("small bottle: Taken.", results[0]);
            Assert.AreEqual("set of keys: Taken.", results[1]);
            Assert.AreEqual("brass lantern: Taken.", results[2]);

            Assert.IsTrue(Inventory.Contains(bottle));
            Assert.IsTrue(Inventory.Contains(keys));
            Assert.IsTrue(Inventory.Contains(lantern));
        }

        [Test]
        public void can_take_all()
        {
            Item bottle = Items.Get<Bottle>();
            Item keys = Items.Get<SetOfKeys>();
            Item food = Items.Get<TastyFood>();
            Item lamp = Items.Get<BrassLantern>();

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
        }

        [Test]
        public void can_take_all_except_object()
        {
            Item bottle = Items.Get<Bottle>();
            Item keys = Items.Get<SetOfKeys>();
            Item food = Items.Get<TastyFood>();
            Item lamp = Items.Get<BrassLantern>();

            parser.Parse("take all except food");

            Assert.IsTrue(Inventory.Contains(bottle, keys, lamp));
            Assert.IsFalse(Inventory.Contains(food));
        }

        [Test]
        public void can_take_all_except_multple_objects()
        {
            Item bottle = Items.Get<Bottle>();
            Item keys = Items.Get<SetOfKeys>();
            Item food = Items.Get<TastyFood>();
            Item lamp = Items.Get<BrassLantern>();

            parser.Parse("take all except food and keys");

            Assert.IsTrue(Inventory.Contains(bottle, lamp));
            Assert.IsFalse(Inventory.Contains(food, keys));
        }

        [Test]
        public void cant_take_something_you_already_have()
        {
            Item bottle = Items.Get<Bottle>();

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
            Assert.AreEqual(Library.CantSeeObject, results[0]);
            
        }

        [Test]
        public void take_bottle_lantern_food_except_bottle()
        {
            var results = parser.Parse("take bottle lantern food except bottle");
            Assert.IsTrue(results.Contains("tasty food: Taken."));
            Assert.IsTrue(results.Contains("brass lantern: Taken."));
        }

        [Test]
        public void take_bottle_lantern_food_except_bottle_with_better_grammer()
        {
            var results = parser.Parse("take bottle, lantern and food except bottle");
            Assert.IsTrue(results.Contains("tasty food: Taken."));
            Assert.IsTrue(results.Contains("brass lantern: Taken."));
        }
    }
}