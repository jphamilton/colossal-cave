using ColossalCave.MyObjects;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class SwitchTests : AdventTestFixture
    {
        [Test]
        public void should_turn_on()
        {
            var lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);
            var results = parser.Parse("switch on lamp");
            Assert.IsTrue(lamp.IsOn);
            Assert.IsTrue(results.Contains("You switch the brass lantern on."));
        }

        [Test]
        public void should_also_turn_on()
        {
            var lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);
            var results = parser.Parse("turn lamp on");
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(lamp.IsOn);
            Assert.IsTrue(results.Contains("You switch the brass lantern on."));
        }

        [Test]
        public void should_turn_off()
        {
            var lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);
            var results = parser.Parse("turn on lamp");
            Assert.IsTrue(lamp.IsOn);
            Assert.IsTrue(results.Contains("You switch the brass lantern on."));
            results = parser.Parse("turn off lamp");
            Assert.IsTrue(results.Contains("You switch the brass lantern off."));
        }

        [Test]
        public void should_also_turn_off()
        {
            var lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);
            var results = parser.Parse("turn on lamp");
            Assert.IsTrue(lamp.IsOn);
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.Contains("You switch the brass lantern on."));
            results = parser.Parse("turn lamp off");
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.Contains("You switch the brass lantern off."));
        }

        [Test]
        public void cannot_turn_on_something_thats_already_on()
        {
            var lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);
            lamp.IsOn = true;
            var results = parser.Parse("turn on lamp");
            Assert.IsTrue(lamp.IsOn);
            Assert.IsTrue(results.Contains("That's already on."));
        }

        [Test]
        public void cannot_turn_off_something_thats_already_off()
        {
            var lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);
            lamp.IsOn = false;
            var results = parser.Parse("turn off lamp");
            Assert.IsFalse(lamp.IsOn);
            Assert.IsTrue(results.Contains("That's already off."));
        }
    }
}
