using Adventure.Net;
using Adventure.Net.Things;
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
    public void the_bottle_is_already_here()
    {
        // not holding bottle, but bottle is in the room
        Execute("drop bottle");

        Assert.Contains($"The small bottle is already here.", ConsoleOut);
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
        Execute("drop all except");
        Assert.Contains("What do you want to drop?", ConsoleOut);
    }

    [Fact]
    public void drop_all_when_inventory_is_empty()
    {
        Execute("drop all");
        Assert.Equal("What do you want to drop?", Line1);
    }

    [Fact]
    public void drop_except_all_is_invalid_order()
    {
        Execute("drop except all");
        Assert.Equal("What do you want to drop?", Line1);
    }

    [Fact]
    public void should_implicit_drop_with_1_item_in_inventory()
    {
        Inventory.Add(Objects.Get<Bottle>());
        Execute("drop");
        var x = ConsoleOut;
        Assert.Equal("(the small bottle)", Line1);
        Assert.Equal("Dropped.", Line2);
    }

    [Fact]
    public void should_implicit_drop_with_1_item_in_inventory_all()
    {
        Inventory.Add(Objects.Get<Bottle>());
        Execute("drop all");
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

        Execute("drop rod");

        Assert.Equal($"({rod.DName})", Line1);
        Assert.Equal("Dropped.", Line2);
    }

    [Fact]
    public void should_remove_from_container_and_drop()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bottle = Objects.Get<Bottle>();
        Inventory.Add(bottle);

        Assert.True(bottle.Parent is Player);
        Execute("put bottle in cage");

        Assert.True(cage.Contains(bottle));
        Assert.False(bottle.Parent is Player);
        Assert.True(bottle.Parent is WickerCage);

        ClearOutput();

        Execute("drop bottle");

        Assert.Contains("(first taking the small bottle out of the wicker cage)", ConsoleOut);
        Assert.False(cage.Contains(bottle));
        Assert.True(bottle.Location == Player.Location);
    }

    [Fact]
    public void should_not_drop_from_closed_transparent_container()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bottle = Objects.Get<Bottle>();
        cage.Add(bottle);

        cage.Open = false;

        Execute("drop bottle");

        Assert.Contains(Messages.NotHoldingThat, ConsoleOut);
        Assert.True(cage.Contains(bottle));
    }

    [Fact]
    public void should_remove_from_container_first()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bottle = Objects.Get<Bottle>();
        cage.Add(bottle);

        cage.Open = true;
        Execute("drop bottle");

        Assert.Equal($"(first taking {bottle.DName} out of {cage.DName})", Line1);
        Assert.Equal("Dropped.", Line2);
        Assert.False(cage.Contains(bottle));
    }

    [Fact]
    public void should_not_drop_from_closed_opaque_container()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bottle = Objects.Get<Bottle>();
        cage.Add(bottle);

        cage.Transparent = false;
        cage.Open = false;

        Execute("drop bottle");

        Assert.Contains(Messages.CantSeeObject, ConsoleOut);
        Assert.True(cage.Contains(bottle));
    }

    [Fact]
    public void should_resolve_to_inventory_on_drop()
    {
        var chain = Object.Get<GoldenChain>();
        chain.MoveToLocation();

        var eggs = Objects.Get<GoldenEggs>();
        eggs.MoveToLocation();

        var nugget = Inventory.Add<LargeGoldNugget>();

        Execute("drop gold");

        Assert.Contains($"({nugget.DName})", ConsoleOut);
    }
}
