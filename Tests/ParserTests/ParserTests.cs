using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Extensions;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.ParserTests;

public class ParserTests : BaseTestFixture
{
    [Fact]
    public void should_handle_bad_verbs()
    {
        var parser = new Parser();
        var result = parser.Preparse("alskjlkja");
        Assert.Equal(Messages.VerbNotRecognized, result.Error);
    }

    [Fact]
    public void should_handle_bad_bad_objects()
    {
        var parser = new Parser();
        var result = parser.Preparse("drop lkjlkjkl");
        Assert.Equal(Messages.CantSeeObject, result.Error);
    }

    [Fact]
    public void should_handle_empty_command()
    {
        var parser = new Parser();
        var result = parser.Preparse("");
        Assert.Equal(Messages.DoNotUnderstand, result.Error);
    }

    [Fact]
    public void should_handle_two_preps()
    {
        Execute("put into on bottle");
        Assert.Contains(Messages.CantSeeObject, ConsoleOut);
    }

    [Fact]
    public void should_handle_NOUN()
    {
        var lamp = Objects.Get<BrassLantern>();
        Inventory.Add(lamp);

        var parser = new Parser();
        var result = parser.Preparse("drop lamp");
        Assert.Single(result.PossibleRoutines);
        Assert.Contains(lamp, result.Objects);
    }

    [Fact]
    public void should_handle_object_not_in_scope()
    {
        var parser = new Parser();
        // trident is not in room
        var result = parser.Preparse("drop trident");
        Assert.Equal(Messages.CantSeeObject, result.Error);
    }

    [Fact]
    public void should_map_to_one_object()
    {
        var lamp = Objects.Get<BrassLantern>();
        Inventory.Add(lamp);

        var parser = new Parser();
        var result = parser.Preparse("drop the shiny brass lamp");
        Assert.Single(result.Objects);
        Assert.Contains(lamp, result.Objects);
    }

    [Fact]
    public void should_swap_indirect_object()
    {
        var lamp = Objects.Get<BrassLantern>();
        Inventory.Add(lamp);

        var parser = new Parser();
        // prep comes first
        var result = parser.Preparse("put down lamp"); // will handle as "put lamp down"
        var x = ConsoleOut;
        Assert.Single(result.Objects);
        Assert.Contains(lamp, result.Objects);
        Assert.Single(result.PossibleRoutines);
    }

    [Fact]
    public void should_resolve_single_from_multiple()
    {
        var red = Objects.Get<RedHat>();
        var white = Objects.Get<WhiteHat>();
        var black = Objects.Get<BlackHat>();

        red.MoveToLocation();
        white.MoveToLocation();
        black.MoveToLocation();

        var parser = new Parser();

        // hat should resolve to red
        var result = parser.Preparse("take red hat");
        Assert.Single(result.Objects);
        Assert.Contains(red, result.Objects);
    }

    [Fact]
    public void should_handle_multiple_objects_with_same_synonyms()
    {
        var red = Objects.Get<RedHat>();
        var white = Objects.Get<WhiteHat>();
        var black = Objects.Get<BlackHat>();

        red.MoveToLocation();
        white.MoveToLocation();
        black.MoveToLocation();

        var parser = new Parser();

        CommandPrompt.FakeInput("red hat");

        Execute("take hat");

        Assert.Contains("Which do you mean", ConsoleOut);
        Assert.Contains("red hat", ConsoleOut);
        Assert.Contains("white hat", ConsoleOut);
        Assert.Contains("black hat", ConsoleOut);

        Assert.Contains(red, Inventory.Items);
    }

    [Fact]
    public void should_handle_multiple_objects_with_same_synonyms_2()
    {
        var red = Objects.Get<RedHat>();
        var white = Objects.Get<WhiteHat>();
        var black = Objects.Get<BlackHat>();

        red.MoveToLocation();
        white.MoveToLocation();
        black.MoveToLocation();

        var parser = new Parser();

        // command is just re-entered when prompted
        CommandPrompt.FakeInput("take the red hat");

        Execute("take hat");
        Assert.Contains("Which do you mean", ConsoleOut);
        Assert.Contains("red hat", ConsoleOut);
        Assert.Contains("white hat", ConsoleOut);
        Assert.Contains("black hat", ConsoleOut);

        Assert.Contains(red, Inventory.Items);
    }

    [Fact]
    public void should_handle_multiple_objects_with_same_synonyms_3()
    {
        var red = Objects.Get<RedHat>();
        var white = Objects.Get<WhiteHat>();
        var black = Objects.Get<BlackHat>();

        red.MoveToLocation();
        white.MoveToLocation();
        black.MoveToLocation();

        var parser = new Parser();

        // specifying two objects when prompted
        CommandPrompt.FakeInput("red black");

        Execute("take hat");
        Assert.Contains("Which do you mean", ConsoleOut);
        Assert.Contains("red hat", ConsoleOut);
        Assert.Contains("white hat", ConsoleOut);
        Assert.Contains("black hat", ConsoleOut);

        Assert.Contains(red, Inventory.Items);
        Assert.Contains(black, Inventory.Items);
    }

    [Fact]
    public void should_handle_multiple_objects_with_same_synonyms_as_many_times_as_needed()
    {
        var red = Objects.Get<RedHat>();
        var white = Objects.Get<WhiteHat>();
        var black = Objects.Get<BlackHat>();

        red.MoveToLocation();
        white.MoveToLocation();
        black.MoveToLocation();

        var parser = new Parser();

        CommandPrompt.FakeInput("hat"); // multiple objects will be returned again!!!
        CommandPrompt.FakeInput("red hat"); // this time we resolve it

        Execute("take hat");
        Assert.Contains("Which do you mean", ConsoleOut);
        Assert.Contains("red hat", ConsoleOut);
        Assert.Contains("white hat", ConsoleOut);
        Assert.Contains("black hat", ConsoleOut);

        Assert.Contains(red, Inventory.Items);
    }

    // compass directions
    [Fact]
    public void should_handle_directions()
    {
        var parser = new Parser();
        var result = parser.Preparse("west");
        Assert.Single(result.PossibleRoutines);
        Assert.True(result.PossibleRoutines[0] is West);
    }

    [Fact]
    public void should_handle_directions_go()
    {
        var parser = new Parser();
        var result = parser.Preparse("go west");
        Assert.Single(result.PossibleRoutines);
        Assert.True(result.PossibleRoutines[0] is West);
    }

    // all/except
    [Fact]
    public void should_take_all()
    {
        var lamp = Objects.Get<BrassLantern>();
        var bottle = Objects.Get<Bottle>();
        var keys = Objects.Get<SetOfKeys>();
        var food = Objects.Get<TastyFood>();

        var parser = new Parser();
        var result = parser.Preparse("take all");
        Assert.Contains(lamp, result.Objects);
        Assert.Contains(bottle, result.Objects);
        Assert.Contains(keys, result.Objects);
        Assert.Contains(food, result.Objects);
    }

    [Fact]
    public void should_take_all_except_keys_and_food()
    {
        var lamp = Objects.Get<BrassLantern>();
        var bottle = Objects.Get<Bottle>();
        var keys = Objects.Get<SetOfKeys>();
        var food = Objects.Get<TastyFood>();

        var parser = new Parser();
        var result = parser.Preparse("take all except keys and food");
        Assert.Contains(lamp, result.Objects);
        Assert.Contains(bottle, result.Objects);
        Assert.DoesNotContain(keys, result.Objects);
        Assert.DoesNotContain(food, result.Objects);
    }

    [Fact]
    public void should_take_except_objects_not_inscope()
    {
        var parser = new Parser();
        var result = parser.Preparse("take all except trident");
        Assert.Equal(Messages.CantSeeObject, result.Error);
    }

    [Fact]
    public void should_except_with_objects_specified()
    {
        var bottle = Objects.Get<Bottle>();
        var keys = Objects.Get<SetOfKeys>();

        var parser = new Parser();
        var result = parser.Preparse("take keys bottle except keys");
        Assert.Single(result.Objects);
        Assert.Contains(bottle, result.Objects);
        Assert.DoesNotContain(keys, result.Objects);
    }

    [Fact]
    public void should_not_include_doors_with_all()
    {
        Location = Room<OutsideGrate>();

        var parser = new Parser();
        var result = parser.Preparse("take all");
        Assert.DoesNotContain(Objects.Get<Grate>(), result.Objects);
    }

    [Fact]
    public void should_handle_doors()
    {
        Location = Room<OutsideGrate>();
        Inventory.Add<SetOfKeys>();

        var parser = new Parser();
        var result = parser.Preparse("unlock grate with keys");
        Assert.Contains(Objects.Get<Grate>(), result.Objects);
        Assert.Contains(Objects.Get<SetOfKeys>(), result.IndirectObjects);
    }

    [Fact]
    public void should_partially_understand_with_object()
    {
        var bottle = Objects.Get<Bottle>();
        var parser = new Parser();

        // second object not present
        var result = parser.Preparse("take bottle trident");

        Assert.Equal($"I only understood you as far as wanting to take {bottle.DName}.", result.Error);
    }

    [Fact]
    public void should_partially_understand_with_indirect()
    {
        var bottle = Inventory.Add<Bottle>();
        var cage = Inventory.Add<WickerCage>();

        // second indirect object not allowed
        Execute("put bottle in cage keys");

        Assert.Contains($"I only understood you as far as wanting to put {bottle.DName} in {cage.DName}.", ConsoleOut);
    }

    [Fact]
    public void MULTIEXCEPT_should_exclude_itself()
    {
        var cage = Inventory.Add<WickerCage>();

        Execute("put cage in cage");
        
        Assert.Contains("You can't put something in itself.", ConsoleOut);
    }

    [Fact]
    public void what_do_you_want_to_do()
    {
        var bottle = Objects.Get<Bottle>();
        Execute("put bottle in");
        Assert.Contains($"What do you want to put {bottle.DName} in?", ConsoleOut);
    }

    [Fact]
    public void what_do_you_want_to_do_2()
    {
        var parser = new Parser();
        var result = parser.Parse("put in");
        Assert.Equal(Messages.CantSeeObject, result.Error);
    }

    [Fact]
    public void what_do_you_want_to_do_3()
    {
        Execute("put bottle keys in");
        Assert.Contains($"What do you want to put those things in?", ConsoleOut);
    }

    [Fact]
    public void objects_should_not_contain_indirect()
    {
        Inventory.Add<WickerCage>();
        var parser = new Parser();
        var result = parser.Parse("put all in cage");
        Assert.True(result.Error == null);
        var cage = Objects.Get<WickerCage>();
        Assert.DoesNotContain(cage, result.Objects);
        Assert.Equal(cage, result.IndirectObject);
        Assert.Contains(Objects.Get<SetOfKeys>(), result.Objects);
        Assert.Contains(Objects.Get<Bottle>(), result.Objects);
        Assert.Contains(Objects.Get<BrassLantern>(), result.Objects);
    }

    [Fact]
    public void MULTIINSIDE_should_remove_from_container()
    {
        Container cage = (Container)Inventory.Add<WickerCage>();
        var bottle = Objects.Get<Bottle>();

        cage.Add(bottle);

        var parser = new Parser();
        var result = parser.Parse("remove bottle from cage");
        Assert.Contains(bottle, result.Objects);
        Assert.Equal(cage, result.IndirectObject);
    }

    [Fact]
    public void MULTIINSIDE_should_remove_from_supporter()
    {
        var table = Objects.Get<Table>();
        table.MoveToLocation();

        var bottle = Objects.Get<Bottle>();
        table.Add(bottle);

        // we now have a table with a bottle on top of it
        var parser = new Parser();
        var result = parser.Parse("remove bottle from table");
        Assert.Contains(bottle, result.Objects);
        Assert.Equal(table, result.IndirectObject);
    }

    [Fact]
    public void MULTIINSIDE_should_not_remove_when_not_inside()
    {
        var cage = Inventory.Add<WickerCage>();

       // var bottle = Objects.Get<Bottle>();

        // we now have a table with a bottle on top of it
        Execute("remove bottle from cage");
        Assert.Contains(Messages.CantSeeObject, ConsoleOut);
    }

    [Fact]
    public void MULTIINSIDE_should_validate()
    {
        var table = Objects.Get<Table>();
        table.MoveToLocation();

        var parser = new Parser();
        var result = parser.Parse("remove table from table");
        Assert.Equal($"You can't remove something from itself.", result.Error);
    }

    [Fact]
    public void MULTIINSIDE_should_handle_partial()
    {
        var keys = Objects.Get<SetOfKeys>();

        Execute("remove keys from");

        Assert.Contains($"What do you want to remove {keys.DName} from?", ConsoleOut);
    }

    [Fact]
    public void NOUN_should_be_valid()
    {
        var bottle = Objects.Get<Bottle>();

        var parser = new Parser();
        var result = parser.Parse("fill bottle");
        Assert.Contains(bottle, result.Objects);
    }

    [Fact]
    public void NOUN_should_not_accept_multiple_objects()
    {
        var parser = new Parser();
        var result = parser.Parse("fill all");
        Assert.Equal("You can't use multiple objects with that verb.", result.Error);
    }

    [Fact]
    public void MULTIHELD_should_pass_non_held_objects()
    {
        // multiheld is only used for Drop (and PutDown which is just a subclass of Drop)
        var keys = Inventory.Add<SetOfKeys>();
        var food = Inventory.Add<TastyFood>();
        var lamp = Objects.Get<BrassLantern>();

        Execute("drop keys food lamp");

        Assert.Contains($"{keys.Name}: Dropped.", ConsoleOut);
        Assert.Contains($"{food.Name}: Dropped.", ConsoleOut);
        Assert.Contains($"{lamp.Name}: {lamp.DName.Capitalize()} is already here.", ConsoleOut);
    }

    [Fact]
    public void MULTIHELD_partial()
    {
        var keys = Inventory.Add<SetOfKeys>();

        // multiheld is only used for Drop (and PutDown which is just a subclass of Drop)
        Execute("drop");
        Assert.Contains($"({keys.DName})", Line1);
        Assert.Contains("Dropped.", Line2);
    }

    [Fact]
    public void MULTIHELD_partial_2()
    {
        var keys = Inventory.Add<SetOfKeys>();

        // multiheld is only used for Drop (and PutDown which is just a subclass of Drop)
        Execute("put down");
        Assert.Contains($"({keys.DName})", ConsoleOut);
        Assert.Contains("Dropped.", ConsoleOut);
    }

    [Fact]
    public void HELD_should_take_object_first()
    {
        var cloak = Objects.Get<BlackCloak>();
        cloak.MoveToLocation();

        Execute("wear cloak");
        Assert.Contains($"(first taking {cloak.DName})", ConsoleOut);
        Assert.Contains($"You put on {cloak.DName}.", ConsoleOut);
    }

    [Fact]
    public void should_pass_objects_in_inventory()
    {
        var cloak = Inventory.Add<BlackCloak>();

        var parser = new Parser();
        var result = parser.Parse("wear cloak");
        Assert.Contains(cloak, result.Objects);
    }

    [Fact]
    public void should_not_accept_multiple_objects()
    {
        var cloak = Inventory.Add<BlackCloak>();
        var keys = Inventory.Add<SetOfKeys>();

        var parser = new Parser();
        var result = parser.Parse("wear cloak keys");
        Assert.Equal("You can't use multiple objects with that verb.", result.Error);
    }

    [Fact]
    public void should_ask_what_to_do_verb_only()
    {
        Execute("put");
        Assert.Contains("What do you want to put?", ConsoleOut);
    }

    [Fact]
    public void should_not_ask_what_to_do_verb_only()
    {
        var parser = new Parser();
        // valid routine with no arguments
        var result = parser.Parse("look");
        Assert.NotEqual("What do you want to look?", result.Error);
    }

    [Fact]
    public void should_ask_what_to_do_verb_prep()
    {
        Execute("put on");
        Assert.Contains("What do you want to put on?", ConsoleOut);
    }

    [Fact]
    public void should_ask_what_to_do_many_objects()
    {
        Execute("put all");
        Assert.Contains("What do you want to put those things in?", ConsoleOut);
    }

    [Fact]
    public void should_ask_what_to_do_many_objects_prep_specified()
    {
        Execute("put all in");
        Assert.Contains("What do you want to put those things in?", ConsoleOut);
    }

    [Fact]
    public void should_interact_with_scenery()
    {
        var stream = Objects.Get<Stream>();
        var parser = new Parser();
        var result = parser.Parse("examine stream");
        Assert.Contains(stream, result.Objects);
    }

}
