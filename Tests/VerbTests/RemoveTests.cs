using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class RemoveTests : BaseTestFixture
{
    // TODO: Nees to test non-transparent containers

    [Fact]
    public void should_not_remove_from_non_container()
    {
        Execute("remove keys from food");
        Assert.Contains(Messages.CantSeeObject, ConsoleOut);
    }

    [Fact]
    public void should_not_remove_from_container_when_not_inside()
    {
        var cage = Objects.Get<WickerCage>();
        cage.MoveToLocation();

        var keys = Objects.Get<SetOfKeys>();
        keys.MoveToLocation();

        Execute("remove keys from cage");

        Assert.Contains(Messages.CantSeeObject, ConsoleOut);
    }

    [Fact]
    public void can_remove_object_from_open_container()
    {
        var cage = Objects.Get<WickerCage>();
        cage.MoveToLocation();

        var keys = Objects.Get<SetOfKeys>();
        cage.Add(keys);

        Execute("remove keys from cage");

        Assert.Contains("Removed.", ConsoleOut);
    }

    [Fact]
    public void should_open_container_first()
    {
        var cage = Objects.Get<WickerCage>();
        cage.MoveToLocation();

        var keys = Objects.Get<SetOfKeys>();
        cage.Add(keys);

        cage.Open = false;

        Execute("remove keys from cage");

        Assert.Contains($"(first opening {cage.DName})", ConsoleOut);
        Assert.Contains("Removed.", ConsoleOut);
        Assert.DoesNotContain(keys, cage.Children);
    }

    [Fact]
    public void should_remove_cloak()
    {
        var cloak = Objects.Get<BlackCloak>();
        Inventory.Add(cloak);
        
        cloak.Worn = true;

        Execute("remove cloak");

        Assert.Contains($"You take off {cloak.DName}.", ConsoleOut);
    }

    [Fact]
    public void should_remove_from_container_in_inventory()
    {
        var keys = Objects.Get<SetOfKeys>();
        var cage = Objects.Get<WickerCage>();
        cage.Add(keys);
        cage.Open = true;

        Inventory.Add(cage);

        Execute("remove keys from cage");

        Assert.DoesNotContain($"(first opening {cage.DName})", ConsoleOut);
        Assert.Contains("Removed.", ConsoleOut);
        Assert.Contains(keys, Inventory.Items);
    }

    [Fact]
    public void should_open_then_remove_from_container_in_inventory()
    {
        var keys = Objects.Get<SetOfKeys>();
        var cage = Objects.Get<WickerCage>();
        cage.Add(keys);
        cage.Open = false;

        Inventory.Add(cage);

        Execute("remove keys from cage");

        Assert.Contains($"(first opening {cage.DName})", ConsoleOut);
        Assert.Contains("Removed.", ConsoleOut);
        Assert.Contains(keys, Inventory.Items);
    }

    [Fact]
    public void should_work_like_take_with_before_after_routines()
    {
        var boots = Objects.Get<HeavyBoots>();
        var cage = Objects.Get<WickerCage>();
        cage.Add(boots);
        cage.MoveToLocation();

        Execute("remove boots from cage");

        Assert.Contains("The boots are too heavy.", ConsoleOut);
        Assert.DoesNotContain(boots, Inventory.Items);
    }
}
