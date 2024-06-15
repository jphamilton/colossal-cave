using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.ParserTests;

public class ImplicitTakeTests : BaseTestFixture
{
    [Fact]
    public void implicit_take_should_trigger_before_after_routines()
    {
        var boots = Objects.Get<HeavyBoots>();
        boots.MoveToLocation();

        Execute("wear boots");

        Assert.Contains("The boots are too heavy.", ConsoleOut);
        Assert.False(Inventory.Contains(boots));
    }

    [Fact]
    public void should_implicitly_take_indirect_object()
    {
        Location = Rooms.Get<OutsideGrate>();

        // keys are on the ground
        var keys = Objects.Get<SetOfKeys>();
        keys.MoveToLocation();

        // key should be implicitly taken
        var result = Execute("unlock grate with key");

        Assert.Contains($"(first taking {keys.DName})", ConsoleOut);
        Assert.Contains($"You unlock the steel grate.", ConsoleOut);
        Assert.Contains(keys, Inventory.Items);
    }

    [Fact]
    public void should_implicitly_take_indirect_object_inside_open_container()
    {
        Location = Rooms.Get<OutsideGrate>();

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var keys = Objects.Get<SetOfKeys>();
        cage.Add(keys);

        Execute("unlock grate with keys");

        Assert.DoesNotContain("You already have that.", ConsoleOut);

        Assert.Contains("(first taking the set of keys out of the wicker cage)", ConsoleOut);
        Assert.Contains("You unlock the steel grate.", ConsoleOut);
        Assert.True(Inventory.Contains(keys));
    }

    [Fact]
    public void should_not_implicitly_take_indirect_object_inside_closed_container()
    {
        Location = Rooms.Get<OutsideGrate>();

        var cage = Objects.Get<WickerCage>();
        var keys = Objects.Get<SetOfKeys>();

        // turn off transparency
        cage.Transparent = false;
        cage.Add(keys);
        cage.Open = false;

        Inventory.Add(cage);

        Execute("unlock grate with keys");

        Assert.False(cage.Open);
        Assert.Contains(Messages.CantSeeObject, ConsoleOut);
        Assert.DoesNotContain("(first taking the set of keys out of the wicker cage)", ConsoleOut);
        Assert.DoesNotContain("You unlock the steel grate.", ConsoleOut);
        Assert.False(keys.Parent is Player);
    }

    [Fact]
    public void should_implicitly_take_item_out_of_container()
    {
        // not sure how to fix this test yet and keep many others passing

        Location = Rooms.Get<Y2>();
        Location.Light = true;

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bottle = Objects.Get<Bottle>();
        cage.Add(bottle);

        Execute("put bottle on rock");

        Assert.Contains($"(first taking {bottle.DName} out of {cage.DName})", ConsoleOut);
        Assert.Contains($"You put {bottle.DName} on the \"Y2\" rock.", ConsoleOut);
    }

    [Fact]
    public void should_print_messages_properly()
    {
        Location = Rooms.Get<HallOfMists>();
        Location.Light = true;

        var axe = Objects.Get<Axe>();
        axe.MoveToLocation();

        var dwarf = Objects.Get<Dwarf>();
        dwarf.MoveToLocation();

        Execute("throw axe at dwarf");

        Assert.Contains("(first taking the dwarvish axe)", ConsoleOut);
        Assert.Contains("(first taking the dwarvish axe)", Line1);
    }

    [Fact]
    public void should_not_fail_all_when_one_cant_be_implicitly_taken()
    {
        // Implicit take current runs for every object, needs to be broken up into individual takes

        var boots = Location.Add<HeavyBoots>();
        var bottle = Objects.Get<Bottle>();
        var lamp = Objects.Get<BrassLantern>();
        var keys = Objects.Get<SetOfKeys>();
        var food = Objects.Get<TastyFood>();

        var cage = (Container)Inventory.Add<WickerCage>();
        Execute("put all in cage");

        Assert.Contains("(first taking the small bottle)", ConsoleOut);
        Assert.Contains("(first taking the brass lantern)", ConsoleOut);
        Assert.Contains("(first taking the set of keys)", ConsoleOut);
        Assert.Contains("(first taking the tasty food)", ConsoleOut);
        Assert.Contains("(first taking the heavy boots)", ConsoleOut);
        Assert.Contains("The boots are too heavy.", ConsoleOut);

        Assert.True(cage.Contains(bottle));
        Assert.True(cage.Contains(lamp));
        Assert.True(cage.Contains(keys));
        Assert.True(cage.Contains(food));

        Assert.False(cage.Contains(boots));

        Assert.DoesNotContain(boots, Inventory.Items);
    }

    [Fact]
    public void multiple_implicit_takes_should_stop_if_one_causes_death()
    {
        var bottle = Objects.Get<Bottle>();
        var lamp = Objects.Get<BrassLantern>();
        var keys = Objects.Get<SetOfKeys>();
        var food = Objects.Get<TastyFood>();

        var cage = (Container)Inventory.Add<WickerCage>();

        lamp.Before<Take>(() =>
        {
            if (Print("The lamp exploded!"))
            {
                throw new DeathException();
            }
            
            return true;
        });

        CommandPrompt.FakeInput("n"); // no, I do not want to live again

        Execute("put all in cage");

        Assert.Contains("(first taking the small bottle)", ConsoleOut);
        Assert.Contains("(first taking the brass lantern)", ConsoleOut);
        Assert.Contains("The lamp exploded!", ConsoleOut);
        
        Assert.DoesNotContain("(first taking the set of keys)", ConsoleOut);
        Assert.DoesNotContain("(first taking the tasty food)", ConsoleOut);
        Assert.DoesNotContain("(first taking the heavy boots)", ConsoleOut);

        Assert.False(cage.Contains(bottle));
        Assert.False(cage.Contains(lamp));
        Assert.False(cage.Contains(keys));
        Assert.False(cage.Contains(food));
    }

    [Fact]
    public void should_first_take_off_the_table()
    {
        var table = Objects.Get<Table>();
        table.MoveToLocation();

        var cage = Inventory.Add<WickerCage>();
        var bottle = Objects.Get<Bottle>();
        
        table.Add(bottle);

        Execute("put bottle in cage");

        Assert.Contains($"(first taking {bottle.DName} off of {table.DName})", ConsoleOut);
        Assert.Contains(bottle, cage.Children);
    }
}
