using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using System.Linq;
using Xunit;

namespace Tests.Verbs;


public class UnlockTests : BaseTestFixture
{
    [Fact]
    public void when_holding_keys_can_unlock_without_specifying_keys_in_input()
    {
        Location = Rooms.Get<OutsideGrate>();
        Inventory.Add(Objects.Get<SetOfKeys>());
        Execute("unlock grate");
        Assert.Equal("(with the set of keys)", Line1);
        Assert.Equal("You unlock the steel grate.", Line2);
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
        
        CommandPrompt.FakeInput("key");

        var result = Execute("unlock grate");
        
        Assert.Contains("You unlock the steel grate.", result.Output[0]);

        Assert.False(grate.Locked);
    }

    [Fact]
    public void should_unlock_with_partial_response()
    {
        Location = Rooms.Get<InsideBuilding>();
        Execute("take all");

        Location = Room<OutsideGrate>();
        var grate = Rooms.Get<Grate>();

        Assert.True(grate.Locked);
        
        CommandPrompt.FakeInput("key");

        var result = Execute("unlock grate");
        
        Assert.Contains("You unlock the steel grate.", result.Output[0]);

        Assert.False(grate.Locked);
    }

    [Fact]
    public void should_implicit_unlock_when_inventory_contains_only_key()
    {
        Location = Rooms.Get<OutsideGrate>();
        Inventory.Add(Objects.Get<SetOfKeys>());

        var grate = Rooms.Get<Grate>();

        Assert.True(grate.Locked);
        var result = Execute("unlock grate");
        Assert.Equal("(with the set of keys)", Line1);
        Assert.Equal("You unlock the steel grate.", Line2);

        Assert.False(grate.Locked);
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
    public void should_not_mimic_inform6_when_attempting_implicit_lock_on_non_lockable()
    {
        // first add to inventory, which would trigger implicit action
        Inventory.Add(Objects.Get<Bottle>());

        var result = Execute("unlock bottle");

        Assert.NotEqual("(with the small bottle)", Line1);
        Assert.Equal("What do you want to unlock the small bottle with?", Line1);
    }

    [Fact]
    public void should_not_mimic_inform6_when_attempting_unlock_non_lockable()
    {
        Execute("unlock bottle");
        Assert.Equal("What do you want to unlock the small bottle with?", Line1);
    }

}
