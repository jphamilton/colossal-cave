using Adventure.Net;
using Adventure.Net.Extensions;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class EmptyTests : BaseTestFixture
{
    [Fact]
    public void cannot_empty_things_that_arent_containers()
    {
        Location = Rooms.Get<InsideBuilding>();
        Execute("empty food");
        Assert.Equal("The tasty food can't contain things.", Line1);
    }

    [Fact]
    public void cannot_empty_something_that_is_already_empty()
    {
        Location = Rooms.Get<InsideBuilding>();
        Execute("empty bottle");
        Assert.Equal("The bottle is already empty!", Line1);
    }

    [Fact]
    public void cannot_empty_something_that_is_closed()
    {
        var cage = Objects.Get<WickerCage>() as Container;
        cage.Add<Bottle>();
        cage.Open = false;
        Inventory.Add(cage);

        Execute("empty cage");
        Assert.Equal("The wicker cage is closed.", Line1);
    }

    [Fact]
    public void can_empty_container_onto_something()
    {
        Location = Rooms.Get<Y2>();

        var keys = Inventory.Add<SetOfKeys>();
        var lamp = Inventory.Add<BrassLantern>();
        var food = Inventory.Add<TastyFood>();
        var bottle = Inventory.Add<Bottle>();
        var cage = (Container)Inventory.Add<WickerCage>();
        var y2 = Objects.Get<Y2Rock>();

        lamp.On = true;
        lamp.Light = true;

        cage.Add(keys);
        cage.Add(food);
        cage.Add(bottle);

        Execute("empty cage onto y2");
        Assert.Contains($"{bottle.Name}: You put {bottle.DName} on {y2.DName}.", ConsoleOut);
        Assert.Contains($"{keys.Name}: You put {keys.DName} on {y2.DName}.", ConsoleOut);
        Assert.Contains($"{food.Name}: You put {food.DName} on {y2.DName}.", ConsoleOut);

        Assert.DoesNotContain(keys, cage.Children);
        Assert.DoesNotContain(food, cage.Children);
        Assert.DoesNotContain(bottle, cage.Children);
    }

    [Fact]
    public void should_open_container_before_emptying()
    {
        Location = Rooms.Get<Y2>();

        var keys = Inventory.Add<SetOfKeys>();
        var lamp = Inventory.Add<BrassLantern>();
        var food = Inventory.Add<TastyFood>();
        var bottle = Inventory.Add<Bottle>();
        var cage = (Container)Inventory.Add<WickerCage>();
        var y2 = Objects.Get<Y2Rock>();

        lamp.On = true;
        lamp.Light = true;

        cage.Add(keys);
        cage.Add(food);
        cage.Add(bottle);
        cage.Open = false;

        Execute("empty cage onto y2");
        Assert.Contains($"(first opening {cage.DName})", ConsoleOut);
        Assert.DoesNotContain($"You open {cage.DName}.", ConsoleOut);
        Assert.Contains($"{bottle.Name}: You put {bottle.DName} on {y2.DName}.", ConsoleOut);
        Assert.Contains($"{keys.Name}: You put {keys.DName} on {y2.DName}.", ConsoleOut);
        Assert.Contains($"{food.Name}: You put {food.DName} on {y2.DName}.", ConsoleOut);

        Assert.True(cage.Open);
        Assert.DoesNotContain(keys, cage.Children);
        Assert.DoesNotContain(food, cage.Children);
        Assert.DoesNotContain(bottle, cage.Children);
    }

    [Fact]
    public void cant_empty_container_onto_bottle()
    {
        Location = Rooms.Get<Y2>();

        var keys = Inventory.Add<SetOfKeys>();
        var lamp = Inventory.Add<BrassLantern>();
        var food = Inventory.Add<TastyFood>();
        var bottle = Inventory.Add<Bottle>();
        var cage = (Container)Inventory.Add<WickerCage>();
        var y2 = Objects.Get<Y2Rock>();

        bottle.MoveToLocation();

        lamp.On = true;
        lamp.Light = true;

        cage.Add(keys);
        cage.Add(food);

        Execute("empty cage onto bottle");

        Assert.Contains($"{keys.Name}: The bottle is only supposed to hold liquids.", ConsoleOut);
        Assert.Contains($"{food.Name}: The bottle is only supposed to hold liquids.", ConsoleOut);

        Assert.Contains(keys, cage.Children);
        Assert.Contains(food, cage.Children);
    }

    [Fact]
    public void cant_empty_non_container_onto_bottle()
    {
        Location = Rooms.Get<Y2>();

        var lamp = Inventory.Add<BrassLantern>();
        var bottle = Inventory.Add<Bottle>();

        lamp.On = true;
        lamp.Light = true;

        // receive routine for bottle should be triggered here
        Execute("empty lamp onto bottle");

        Assert.Contains($"{lamp.DName.Capitalize()} can't contain things.", ConsoleOut);
    }

    [Fact]
    public void cant_empty_an_empty_container()
    {
        var table = Objects.Get<Table>();
        table.MoveToLocation();

        var box = Inventory.Add<OpaqueBox>();

        // receive routine for bottle should be triggered here
        Execute("empty box on table");

        Assert.Contains($"{box.DName.Capitalize()} is empty already.", ConsoleOut);
    }

    [Fact]
    public void cant_empty_an_emptyasasasa_container()
    {
        var bottle = Inventory.Add<Bottle>();

        Execute("fill bottle");
        ClearOutput();

        // receive routine for bottle should be triggered here
        Execute("empty bottle into bottle");

        Assert.Contains($"That wouldn't empty anything.", ConsoleOut);
    }

    [Fact]
    public void can_empty_bottle()
    {
        var bottle = Inventory.Add<Bottle>();

        Execute("fill bottle");
        ClearOutput();

        // receive routine for bottle should be triggered here
        Execute("empty bottle");

        Assert.Contains($"Your bottle is now empty and the ground is now wet.", ConsoleOut);
    }
}