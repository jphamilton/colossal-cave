using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class DropTests : BaseTestFixture
{
    [Fact]
    public void should_handle_drop()
    {
        var bottle = Objects.Get<Bottle>();
        bottle.Remove();
        Inventory.Add(bottle);

        // not holding bottle, but bottle is in the room
        var result = Execute("drop bottle");

        Assert.Contains($"Dropped.", ConsoleOut);
    }

    [Fact]
    public void should_handle_multiheld_not_held_but_in_room()
    {
        var bottle = Objects.Get<Bottle>();

        // not holding bottle, but bottle is in the room
        var result = Execute("drop bottle");

        Assert.Contains($"The {bottle.Name} is already here.", ConsoleOut);
    }

    [Fact]
    public void should_not_drop_something_not_in_scope()
    {
        Assert.False(Inventory.Contains("cage"));
        Execute("drop cage");
        Assert.Equal(Messages.CantSeeObject, Line1);
    }

    [Fact]
    public void should_drop_everything()
    {
        Execute("take all");

        Assert.True(Inventory.Contains("bottle"));
        Assert.True(Inventory.Contains("food"));
        Assert.True(Inventory.Contains("keys"));
        Assert.True(Inventory.Contains("lamp"));

        Execute("drop all");

        Assert.False(Inventory.Contains("bottle"));
        Assert.False(Inventory.Contains("food"));
        Assert.False(Inventory.Contains("keys"));
        Assert.False(Inventory.Contains("lamp"));

        Assert.Contains("set of keys: Dropped.", ConsoleOut);
        Assert.Contains("tasty food: Dropped.", ConsoleOut);
        Assert.Contains("brass lantern: Dropped.", ConsoleOut);
        Assert.Contains("small bottle: Dropped.", ConsoleOut);
    }

    [Fact]
    public void drop_all_except_object_not_specified()
    {
        var result = Execute("drop all except");
        Assert.Equal("What do you want to drop those things in?", Line1);
    }

    [Fact]
    public void drop_all_when_inventory_is_empty()
    {
        Execute("drop all");
        Assert.Equal("What do you want to drop those things in?", Line1);
    }

    [Fact]
    public void drop_except_all_is_invalid_order()
    {
        var result = Execute("drop except all");
        Assert.Equal(Messages.CantSeeObject, Line1);
    }

    [Fact]
    public void should_implicit_drop_with_1_item_in_inventory()
    {
        Inventory.Add(Objects.Get<Bottle>());
        var result = Execute("drop");
        Assert.Equal("(the small bottle)", Line1);
        Assert.Equal("Dropped.", Line2);
    }

    [Fact]
    public void should_implicit_drop_with_1_item_in_inventory_all()
    {
        Inventory.Add(Objects.Get<Bottle>());
        var result = Execute("drop all");
        Assert.Equal("(the small bottle)", Line1);
        Assert.Equal("Dropped.", Line2);
    }

    [Fact]
    public void favor_held_object_on_name_conflict()
    {
        var mark = Objects.Get<BlackMarkRod>();
        mark.MoveToLocation();

        var rod = Objects.Get<BlackRod>();
        Inventory.Add(rod);

        var result = Execute("drop rod");
        Assert.Equal("Dropped.", Line1);
    }

    [Fact]
    public void should_remove_from_container_and_drop()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bottle = Objects.Get<Bottle>();
        Inventory.Add(bottle);

        Execute("put bottle in cage");

        Assert.True(cage.Contains(bottle));
        Assert.False(bottle.Location == CurrentRoom.Location);

        ClearOutput();

        Execute("drop bottle");

        var x = ConsoleOut;

        Assert.False(cage.Contains(bottle));
        Assert.True(bottle.Location == CurrentRoom.Location);
    }
}
