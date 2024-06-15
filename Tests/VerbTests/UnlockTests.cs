using Adventure.Net;
using Adventure.Net.Extensions;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.Verbs;


public class UnlockTests : BaseTestFixture
{
    [Fact]
    public void should_not_implict_unlock_grate_when_inventory_contains_multiple_items()
    {
        Location = Rooms.Get<OutsideGrate>();
        var grate = Objects.Get<Grate>();
        Inventory.Add(Objects.Get<SetOfKeys>());
        Inventory.Add(Objects.Get<BrassLantern>());
        Execute("unlock grate");
        Assert.Contains($"What do you want to unlock {grate.DName} with?", Line1);
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

        Assert.True(grate.Locked);

        ClearOutput();

        Execute("unlock grate");

        Assert.Contains($"What do you want to unlock {grate.DName} with?", ConsoleOut);

        Assert.True(grate.Locked);
    }

    [Fact]
    public void should_unlock_with_partial_response()
    {
        Location = Room<OutsideGrate>();

        Inventory.Add(Objects.Get<SetOfKeys>());
        Inventory.Add(Objects.Get<BrassLantern>());
        Inventory.Add(Objects.Get<TastyFood>());

        var grate = Rooms.Get<Grate>();

        Assert.True(grate.Locked);

        CommandPrompt.FakeInput("key");

        Execute("unlock grate");

        Assert.Contains($"What do you want to unlock {grate.DName} with?", ConsoleOut);
        Assert.Contains("You unlock the steel grate.", ConsoleOut);

        Assert.False(grate.Locked);
    }

    [Fact]
    public void should_implicit_unlock_when_inventory_contains_only_key()
    {
        Location = Rooms.Get<OutsideGrate>();
        Inventory.Add(Objects.Get<SetOfKeys>());

        var grate = Rooms.Get<Grate>();

        Assert.True(grate.Locked);

        Execute("unlock grate");

        Assert.Equal("(with the set of keys)", Line1);
        Assert.Equal("You unlock the steel grate.", Line2);

        Assert.False(grate.Locked);
    }

    [Fact]
    public void should_not_unlock_with_non_key()
    {
        Location = Rooms.Get<OutsideGrate>();
        var bottle = Inventory.Add<Bottle>();

        var grate = Rooms.Get<Grate>();

        Assert.True(grate.Locked);

        Execute("unlock grate with bottle");

        Assert.Contains($"{bottle.DName.Capitalize()} doesn't seem to fit the lock.", ConsoleOut);

        Assert.True(grate.Locked);
    }

    [Fact]
    public void should_not_unlock_twice()
    {

        Location = Rooms.Get<OutsideGrate>();
        Inventory.Add(Objects.Get<SetOfKeys>());

        var grate = Rooms.Get<Grate>();
        grate.Locked = false;

        var result = Execute("unlock grate with keys");
        Assert.Equal("That's unlocked at the moment.", Line1);

        Assert.False(grate.Locked);
    }

    [Fact]
    public void should_not_unlock_unlockable_object()
    {
        // first add to inventory, which would trigger implicit action
        var bottle = Objects.Get<Bottle>();
        Inventory.Add(bottle);

        Execute("unlock bottle");

        Assert.Equal($"(with {bottle.DName})", Line1);
        Assert.Equal($"The small bottle doesn't seem to be something you can unlock.", Line2);
    }

    [Fact]
    public void should_not_unlock_unlockable_object_2()
    {
        // first add to inventory, which would trigger implicit action
        var bottle = Objects.Get<Bottle>();
        var keys = Object.Get<SetOfKeys>();
        
        Inventory.Add(bottle);
        Inventory.Add(keys);

        Execute("unlock bottle");

        Assert.Equal($"What do you want to unlock {bottle.DName} with?", Line1);
    }

    [Fact]
    public void should_not_offer_to_unlock_grate()
    {
        Location = Rooms.Get<OutsideGrate>();
        var grate = Objects.Get<Grate>();
        Inventory.Add(Objects.Get<SetOfKeys>());
        Inventory.Add(Objects.Get<BrassLantern>());

        Execute("unlock");

        Assert.Contains($"What do you want to unlock?", ConsoleOut);
    }
}
