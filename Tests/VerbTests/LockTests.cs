using Adventure.Net;
using Adventure.Net.Extensions;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class LockTests : BaseTestFixture
{
    [Fact]
    public void what_to_lock_with()
    {
        Execute("lock bottle");
        Assert.Contains($"What do you want to lock the small bottle with?", ConsoleOut);
    }

    [Fact]
    public void should_not_lock_unlockable()
    {
        var keys = Inventory.Add<SetOfKeys>();
        Execute("lock bottle with keys");
        Assert.Contains("That doesn't seem to be something you can lock.", ConsoleOut);
    }

    [Fact]
    public void should_not_already_locked()
    {
        Location = Rooms.Get<OutsideGrate>();
        var keys = Inventory.Add<SetOfKeys>();
        Execute("lock grate with keys");
        Assert.Contains("It's locked at the moment.", ConsoleOut);
    }

    [Fact]
    public void should_implicit_take_keys_and_lock()
    {
        Location = Rooms.Get<OutsideGrate>();
        
        var keys = Objects.Get<SetOfKeys>();
        keys.MoveToLocation();

        var grate = Objects.Get<Grate>();
        grate.Locked = false;
        
        Execute("lock grate with keys");
        Assert.Contains($"(first taking {keys.DName})", ConsoleOut);
        Assert.Contains($"You lock {grate.DName}.", ConsoleOut);
    }

    [Fact]
    public void should_lock_grate_with_keys_implicit()
    {
        Location = Rooms.Get<OutsideGrate>();

        var keys = Inventory.Add<SetOfKeys>();

        var grate = Objects.Get<Grate>();
        grate.Locked = false;

        Execute("lock grate");
        
        Assert.Contains($"(with {keys.DName})", ConsoleOut);
        Assert.Contains($"You lock {grate.DName}.", ConsoleOut);
        Assert.True(grate.Locked);
    }

    [Fact]
    public void should_not_lock()
    {
        Location = Rooms.Get<OutsideGrate>();

        // more than one object in inventory
        var bottle = Inventory.Add<Bottle>();

        var grate = Objects.Get<Grate>();
        grate.Locked = false;

        Execute("lock grate");
        
        Assert.Contains($"(with {bottle.DName})", ConsoleOut);
        Assert.Contains($"{bottle.DName.Capitalize()} doesn't seem to fit the lock.", ConsoleOut);
    }
}
