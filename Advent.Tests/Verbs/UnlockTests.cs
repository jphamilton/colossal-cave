using System.IO;
using ColossalCave.MyObjects;
using ColossalCave.MyRooms;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.Verbs
{
    [TestFixture]
    public class UnlockTests : AdventTestFixture
    {
        [Test]
        public void when_holding_keys_can_unlock_without_specifying_keys_in_input()
        {
            Location = Rooms.Get<OutsideGrate>();
            Inventory.Add(Objects.Get<SetOfKeys>());
            var results = parser.Parse("unlock grate");
            results.ShouldContain("(with the set of keys)");
            results.ShouldContain("You unlock the steel grate.");
        }

        [Test]
        public void should_unlock()
        {
            Location = Rooms.Get<OutsideGrate>();
            Inventory.Add(Objects.Get<SetOfKeys>());
            var results = parser.Parse("unlock grate with keys");
            results.ShouldContain("You unlock the steel grate.");
            results.ShouldNotContain("(first taking the steel grate)"); // from bug
            results.CountShouldBe(1);
        }

        [Test]
        public void should_not_unlock_door_automatically_when_inventory_contains_stuff_other_than_the_key()
        {
            
            var prompt = new FakeCommandPrompt("keys");
            Context.CommandPrompt = prompt;
            
            Location = Rooms.Get<InsideBuilding>();
            parser.Parse("take all");

            Location = Rooms.Get<OutsideGrate>();
            var grate = Rooms.Get<Grate>();

            Assert.True(grate.IsLocked);
            var results = parser.Parse("unlock grate");
            results.ShouldContain("You unlock the steel grate.");
            
            Assert.IsFalse(grate.IsLocked);
        }

        [Test]
        public void should_automatically_unlock_because_inventory_only_contains_the_key()
        {

            Location = Rooms.Get<OutsideGrate>();
            Inventory.Add(Objects.Get<SetOfKeys>());
            
            var grate = Rooms.Get<Grate>();

            Assert.True(grate.IsLocked);
            var results = parser.Parse("unlock grate");
            results.ShouldContain("(with the set of keys)");
            results.ShouldContain("You unlock the steel grate.");

            Assert.IsFalse(grate.IsLocked);
        }

        
    }
}
