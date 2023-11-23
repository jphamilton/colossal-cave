using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class TakeTests : BaseTestFixture
{
    [Fact]
    public void can_take_one_object()
    {
        var bottle = Objects.Get<Bottle>();

        Execute("take bottle");

        Assert.Equal("Taken.", Line1);

        Assert.True(Inventory.Contains(bottle));

        Assert.False(ObjectMap.Contains(Location, bottle));
    }

    [Fact]
    public void cannot_take_something_which_is_not_around()
    {
        var cage = Objects.Get<WickerCage>();

        Execute("take cage");

        Assert.Equal("You can't see any such thing.", Line1);

        Assert.False(Inventory.Contains(cage));
    }

    [Fact]
    public void cannot_take_scenic_objects()
    {
        Execute("take building");

        Assert.Equal("That's hardly portable.", Line1);
    }

    [Fact]
    public void cannot_take_static_objects()
    {
        Location = Room<OutsideGrate>();
        var result = Execute("take grate");
        Assert.Equal("That's fixed in place.", Line1);
    }

    [Fact]
    public void can_take_multiple_objects()
    {
        var bottle = Objects.Get<Bottle>();
        var keys = Objects.Get<SetOfKeys>();

        Execute("take bottle and keys");

        Assert.Equal("small bottle: Taken.", Line1);
        Assert.Equal("set of keys: Taken.", Line2);

        Assert.True(Inventory.Contains(bottle));
        Assert.True(Inventory.Contains(keys));
    }

    [Fact]
    public void can_take_comma_delimited()
    {
        var bottle = Objects.Get<Bottle>();
        var keys = Objects.Get<SetOfKeys>();

        Execute("take bottle,keys");

        Assert.Equal("small bottle: Taken.", Line1);
        Assert.Equal("set of keys: Taken.", Line2);

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

        Assert.Equal("small bottle: Taken.", Line1);
        Assert.Equal("set of keys: Taken.", Line2);
        Assert.Equal("brass lantern: Taken.", Line3);

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

        Assert.Equal("small bottle: Taken.", Line1);
        Assert.Equal("set of keys: Taken.", Line2);
        Assert.Equal("brass lantern: Taken.", Line3);

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

        Assert.True(Inventory.Contains(bottle));
        Assert.True(Inventory.Contains(keys));
        Assert.True(Inventory.Contains(lamp));
        Assert.False(Inventory.Contains(food));
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

        bottle.Remove();
        Inventory.Add(bottle);

        Execute("take bottle");

        Assert.Equal("You already have that.", Line1);

    }

    [Fact]
    public void take_except()
    {
        Execute("take except");
        Assert.Equal(Messages.CantSeeObject, Line1);

    }

    [Fact]
    public void take_bottle_lantern_food_except_bottle()
    {
        Execute("take bottle lantern food except bottle");
        Assert.Contains("I only understood you as far as wanting to take the small bottle.", ConsoleOut);
    }

    [Fact]
    public void implicit_take()
    {
        // eat command requires that object be in players inventory
        // here it's not, but it is in the room - so take it for
        // the player automatically
        var tastyFood = Objects.Get<TastyFood>();

        Assert.False(Inventory.Contains(tastyFood));

        Execute("eat food");

        Assert.Equal("(first taking the tasty food)", Line1);
        Assert.Equal("Delicious!", Line2);

        Assert.False(CurrentRoom.Has<TastyFood>());
        Assert.False(Inventory.Contains(tastyFood));

    }

    [Fact]
    public void implicit_take_2()
    {
        Execute("take all except lamp");
        var lamp = Objects.Get<BrassLantern>();

        Assert.False(Inventory.Contains(lamp));

        ClearOutput();

        Execute("take");

        Assert.Equal("(the brass lantern)", Line1);
        Assert.Equal("Taken.", Line2);

        Assert.True(Inventory.Contains(lamp));
    }

    [Fact]
    public void implicit_take_is_blocked_by_before()
    {
        var blocked = "The food disappears before you can take it.";

        // eat command requires that object be in players inventory
        // here it's not, but it is in the room - so take it for
        // the player automatically
        var tastyFood = Objects.Get<TastyFood>();

        tastyFood.Before<Take>(() =>
        {
            Print(blocked);
            return true;
        });

        Assert.False(Inventory.Contains(tastyFood));

        var result = Execute("eat food");

        // Assert.Equal("(first taking the tasty food)", Line1);
        Assert.Equal(blocked, Line1);

        Assert.True(CurrentRoom.Has<TastyFood>());
        Assert.False(Inventory.Contains(tastyFood));
    }


    [Fact]
    public void no_implicit_take()
    {
        var tastyFood = Objects.Get<TastyFood>();

        Inventory.Add(tastyFood);

        Execute("eat food");

        Assert.Equal("Delicious!", Line1);

        Assert.False(CurrentRoom.Has<TastyFood>());
        Assert.False(Inventory.Contains(tastyFood));

    }
}
