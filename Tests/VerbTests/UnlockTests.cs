using Adventure.Net;
using ColossalCave.Things;
using ColossalCave.Places;
using System.Linq;
using Xunit;

namespace Tests.Verbs
{

    public class UnlockTests : BaseTestFixture
    {
        [Fact]
        public void when_holding_keys_can_unlock_without_specifying_keys_in_input()
        {
            Location = Rooms.Get<OutsideGrate>();
            Inventory.Add(Objects.Get<SetOfKeys>());
            Execute("unlock grate");
            Assert.Equal("(with the set of keys)", Line(1));
            Assert.Equal("You unlock the steel grate.", Line(2));
        }

        [Fact]
        public void should_unlock()
        {
            Location = Rooms.Get<OutsideGrate>();
            Inventory.Add(Objects.Get<SetOfKeys>());
            Execute("unlock grate with keys");
            Assert.Contains("You unlock the steel grate.", ConsoleOut);
            Assert.DoesNotContain("(first taking the steel grate)", ConsoleOut); // from bug
        }

        [Fact]
        public void should_not_implicit_unlock_when_inventory_contains_more_than_key()
        {
            Location = Rooms.Get<InsideBuilding>();
            Execute("take all");

            Location = Room<OutsideGrate>();
            var grate = Rooms.Get<Grate>();

            Assert.True(grate.IsLocked);
            var result = Execute("unlock grate");
            Assert.Contains("What do you want to unlock the steel grate with?", result.Output.Single());

            Assert.True(grate.IsLocked);
        }

        [Fact]
        public void should_implicit_unlock_when_inventory_contains_only_key()
        {
            Location = Rooms.Get<OutsideGrate>();
            Inventory.Add(Objects.Get<SetOfKeys>());

            var grate = Rooms.Get<Grate>();

            Assert.True(grate.IsLocked);
            var result = Execute("unlock grate");
            Assert.Equal("(with the set of keys)", Line(1));
            Assert.Equal("You unlock the steel grate.", Line(2));

            Assert.False(grate.IsLocked);
        }

        [Fact]
        public void should_not_unlock_twice()
        {

            Location = Rooms.Get<OutsideGrate>();
            Inventory.Add(Objects.Get<SetOfKeys>());

            var grate = Rooms.Get<Grate>();
            grate.IsLocked = false;

            var result = Execute("unlock grate with keys");
            Assert.Equal("That's unlocked at the moment.", Line(1));

            Assert.False(grate.IsLocked);
        }

        [Fact]
        public void should_not_mimic_inform6_when_attempting_implicit_lock_on_non_lockable()
        {
            // first add to inventory, which would trigger implicit action
            Inventory.Add(Objects.Get<Bottle>());

            var result = Execute("unlock bottle");
            
            Assert.NotEqual("(with the small bottle)", Line(1));
            Assert.Equal("What do you want to unlock the small bottle with?", Line(1));
        }

        [Fact]
        public void should_not_mimic_inform6_when_attempting_unlock_non_lockable()
        {
            Execute("unlock bottle");
            Assert.Equal("What do you want to unlock the small bottle with?", Line(1));
        }

    }
}
