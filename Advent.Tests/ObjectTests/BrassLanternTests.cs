using ColossalCave.MyObjects;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.ObjectTests
{
    [TestFixture]
    public class BrassLanternTests : AdventTestFixture
    {
        private BrassLantern lamp;

        protected override void OnSetUp()
        {
            lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);
        }

        protected override void OnTearDown()
        {
            Inventory.Clear();
        }
        
        [Test]
        public void should_accept_fresh_batteries()
        {
            var fresh = Objects.Get<FreshBatteries>();
            Inventory.Add(fresh);
            lamp.PowerRemaining = 0;
            var results = parser.Parse("put batteries in lamp");
            Assert.AreEqual(lamp.PowerRemaining, 2500);
            Assert.IsTrue(results.Contains("I'm taking the liberty of replacing the batteries."));
            var old = Objects.Get<OldBatteries>();
            Assert.IsTrue(old.InScope);
            Assert.IsTrue(fresh.HaveBeenUsed);
        }

        [Test]
        public void should_reject_old_batteries()
        {
            Inventory.Add(Objects.Get<OldBatteries>());
            var results = parser.Parse("put batteries in lamp");
            results.ShouldContain("Those batteries are dead; they won't do any good at all.");            
        }

        [Test]
        public void cannot_accept_anything_other_than_batteries()
        {
            var bottle = Objects.Get<Bottle>();
            Inventory.Add(bottle);
            var results = parser.Parse("put bottle in lamp");
            results.ShouldContain("The only thing you might successfully put in the lamp is a fresh pair of batteries.");
        }
        [Test]
        public void should_not_switch_on_if_batteries_are_dead()
        {
            lamp.PowerRemaining = 0;
            var results = parser.Parse("turn on lamp");
            Assert.IsFalse(lamp.IsOn);
            results.ShouldContain("Unfortunately, the batteries seem to be dead.");
        }

        
    }
}
