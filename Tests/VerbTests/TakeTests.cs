using Adventure.Net;
using ColossalCave.Objects;
using ColossalCave.Places;
using Xunit;

namespace Tests.VerbTests
{
    public class TakeTests : BaseTestFixture
    {
        [Fact]
        public void can_take_one_object()
        {
            var bottle = Objects.Get<Bottle>();

            Execute("take bottle");

            Assert.Equal("Taken.", Line(1));

            Assert.True(Inventory.Contains(bottle));
            Assert.False(Room.Objects.Contains(bottle));
        }

        [Fact]
        public void cannot_take_something_which_is_not_around()
        {
            var cage = Objects.Get<WickerCage>();
            
            Execute("take cage");

            Assert.Equal("You can't see any such thing.", ConsoleOut);

            Assert.False(Inventory.Contains(cage));
        }

        [Fact]
        public void cannot_take_scenic_objects()
        {
            Execute("take building");
            
            Assert.Equal("That's hardly portable.", Line(1));
        }

        [Fact]
        public void cannot_take_static_objects()
        {
            Room = Rooms.Get<OutsideGrate>();
            var result = Execute("take grate");
            Assert.Equal("That's fixed in place.", Line(1));
        }

        [Fact]
        public void can_take_multiple_objects()
        {
            var bottle = Objects.Get<Bottle>();
            var keys = Objects.Get<SetOfKeys>();

            Execute("take bottle and keys");

            Assert.Equal("small bottle: Taken.", Line(1));
            Assert.Equal("set of keys: Taken.", Line(2));

            Assert.True(Inventory.Contains(bottle));
            Assert.True(Inventory.Contains(keys));
        }

        [Fact]
        public void can_take_comma_delimited()
        {
            var bottle = Objects.Get<Bottle>();
            var keys = Objects.Get<SetOfKeys>();

            Execute("take bottle,keys");

            Assert.Equal("small bottle: Taken.", Line(1));
            Assert.Equal("set of keys: Taken.", Line(2));

            Assert.True(Inventory.Contains(bottle));
            Assert.True(Inventory.Contains(keys));
        }

        [Fact]
        public void can_take_multiple_objects_using_and()
        {
            var bottle = Objects.Get<Bottle>();
            var keys = Objects.Get<SetOfKeys>();
            var lantern = Objects.Get<BrassLantern>();

            Execute("take bottle and keys and lantern");

            Assert.Equal("small bottle: Taken.", Line(1));
            Assert.Equal("set of keys: Taken.", Line(2));
            Assert.Equal("brass lantern: Taken.", Line(3));

            Assert.True(Inventory.Contains(bottle));
            Assert.True(Inventory.Contains(keys));
            Assert.True(Inventory.Contains(lantern));
        }

        [Fact]
        public void can_take_multiple_objects_using_comma_and()
        {
            var bottle = Objects.Get<Bottle>();
            var keys = Objects.Get<SetOfKeys>();
            var lantern = Objects.Get<BrassLantern>();

            Execute("take bottle, keys and lantern");

            Assert.Equal("small bottle: Taken.", Line(1));
            Assert.Equal("set of keys: Taken.", Line(2));
            Assert.Equal("brass lantern: Taken.", Line(3));

            Assert.True(Inventory.Contains(bottle));
            Assert.True(Inventory.Contains(keys));
            Assert.True(Inventory.Contains(lantern));
        }

        [Fact]
        public void can_take_all()
        {
            var bottle = Objects.Get<Bottle>();
            var keys = Objects.Get<SetOfKeys>();
            var food = Objects.Get<TastyFood>();
            var lamp = Objects.Get<BrassLantern>();

            Execute("take all");

            Assert.True(Inventory.Contains(bottle, keys, lamp, food));

            Assert.Contains("set of keys: Taken.", ConsoleOut);
            Assert.Contains("tasty food: Taken.", ConsoleOut);
            Assert.Contains("brass lantern: Taken.", ConsoleOut);
            Assert.Contains("small bottle: Taken.", ConsoleOut);
        }

        [Fact]
        public void can_take_all_except_object()
        {
            var bottle = Objects.Get<Bottle>();
            var keys = Objects.Get<SetOfKeys>();
            var food = Objects.Get<TastyFood>();
            var lamp = Objects.Get<BrassLantern>();

            Execute("take all except food");

            Assert.True(bottle.InInventory);
            Assert.True(keys.InInventory);
            Assert.True(lamp.InInventory);
            Assert.False(food.InInventory);
        }

        [Fact]
        public void can_take_all_except_multple_objects()
        {
            var bottle = Objects.Get<Bottle>();
            var keys = Objects.Get<SetOfKeys>();
            var food = Objects.Get<TastyFood>();
            var lamp = Objects.Get<BrassLantern>();

            var result = Execute("take all except food and keys");

            Assert.True(Inventory.Contains(bottle, lamp));
            Assert.False(Inventory.Contains(food, keys));
        }

        [Fact]
        public void cant_take_something_you_already_have()
        {
            var bottle = Objects.Get<Bottle>();

            Room.Objects.Remove(bottle);
            Inventory.Add(bottle);

            Execute("take bottle");

            Assert.Equal("You already have that.", Line(1));

        }

        [Fact]
        public void take_except()
        {
            Execute("take except");
            Assert.Equal(Messages.CantSeeObject, ConsoleOut);

        }

        [Fact]
        public void take_bottle_lantern_food_except_bottle()
        {
            Execute("take bottle lantern food except bottle");
            Assert.Contains("I only understood you as far as wanting to take the small bottle.", ConsoleOut);
        }

        [Fact]
        public void should_take_held_object_if_in_room()
        {
            // eat command requires that object be in players inventory
            // here it's not, but it is in the room - so take it for
            // the player automatically
            var tastyFood = Objects.Get<TastyFood>();

            Assert.False(Inventory.Contains(tastyFood));
            
            var result = Execute("eat food");

            Assert.Equal("(first taking the tasty food)", Line(1));
            Assert.Equal("Delicious!", Line(2));

            Assert.False(Room.Contains(tastyFood));
        }
    }
}
