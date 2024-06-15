using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using ColossalCave.Things;
using System.Collections.Generic;
using Xunit;

namespace Tests.ParserTests;

public class MultipleMatchedItemsTests : BaseTestFixture
{
    [Fact]
    public void should_handle_two_objects_with_same_name()
    {
        var room = Player.Location;

        var red = Objects.Get<RedHat>();
        red.MoveToLocation();

        var black = Objects.Get<BlackHat>();
        black.MoveToLocation();

        CommandPrompt.FakeInput("red");

        Execute("take hat");

        Assert.Contains("Which do you mean, the red hat or the black hat?", ConsoleOut);
        Assert.Contains("Taken.", ConsoleOut);
        Assert.Contains(red, Inventory.Items);
    }

    [Fact]
    public void should_handle_more_than_two_objects_with_same_name()
    {
        var room = Player.Location;

        var red = Objects.Get<RedHat>();
        red.MoveToLocation();

        var black = Objects.Get<BlackHat>();
        black.MoveToLocation();

        var white = Objects.Get<WhiteHat>();
        white.MoveToLocation();

        CommandPrompt.FakeInput("white");

        Execute("take hat");

        Assert.Contains("Which do you mean, the red hat, the black hat or the white hat?", ConsoleOut);
        Assert.Contains("Taken.", ConsoleOut);
        Assert.Contains(white, Inventory.Items);
    }

    [Fact]
    public void should_handle_two_objects_with_same_name_in_except_clause()
    {
        var room = Player.Location;

        var red = Objects.Get<RedHat>();
        red.MoveToLocation();

        var black = Objects.Get<BlackHat>();
        black.MoveToLocation();

        var white = Objects.Get<WhiteHat>();
        white.MoveToLocation();

        CommandPrompt.FakeInput("white");

        Execute("take all except hat");

        // need to for bad responses and stuff

        Assert.DoesNotContain("white hat: Taken.", ConsoleOut);
        Assert.DoesNotContain(white, Inventory.Items);
        Assert.Contains(red, Inventory.Items);
        Assert.Contains(black, Inventory.Items);

    }

    [Fact]
    public void should_handle_command_reentry_in_except_clause_that_has_multiple_matching_items()
    {
        var room = Player.Location;

        var red = Objects.Get<RedHat>();
        red.MoveToLocation();

        var black = Objects.Get<BlackHat>();
        black.MoveToLocation();

        var white = Objects.Get<WhiteHat>();
        white.MoveToLocation();

        // this should bail out of the except clause handler
        CommandPrompt.FakeInput("take white hat");

        Execute("take all except hat");

        Assert.DoesNotContain("white hat: Taken.", ConsoleOut);
        Assert.Contains(white, Inventory.Items);
        Assert.DoesNotContain(red, Inventory.Items);
        Assert.DoesNotContain(black, Inventory.Items);
    }

    [Fact]
    public void should_handle_a_specific_multiple_matched_item_entered_using_two_words()
    {
        var room = Player.Location;

        var red = Objects.Get<RedHat>();
        red.MoveToLocation();

        var black = Objects.Get<BlackHat>();
        black.MoveToLocation();

        var white = Objects.Get<WhiteHat>();
        white.MoveToLocation();

        Execute("take white hat");

        Assert.Contains("Taken.", ConsoleOut);
    }

    [Fact]
    public void should_handle_bad_command_reentry_in_except_clause_that_has_multiple_matching_items()
    {
        var room = Player.Location;

        var red = Objects.Get<RedHat>();
        red.MoveToLocation();

        var black = Objects.Get<BlackHat>();
        black.MoveToLocation();

        var white = Objects.Get<WhiteHat>();
        white.MoveToLocation();

        CommandPrompt.FakeInput("donkey");

        Execute("take all except hat");

        Assert.Contains(Messages.CantSeeObject, ConsoleOut);
    }

    [Fact]
    public void should_repeatedly_try_to_resolve_multiple_objects()
    {
        var red = Objects.Get<RedHat>();
        red.MoveToLocation();

        var black = Objects.Get<BlackHat>();
        black.MoveToLocation();

        var white = Objects.Get<WhiteHat>();
        white.MoveToLocation();

        CommandPrompt.FakeInput("hat");

        Execute("take hat");

        Assert.Contains("Which do you mean, the red hat, the black hat or the white hat?", Line1);
        Assert.Contains("Which do you mean, the red hat, the black hat or the white hat?", Line2);
    }

    [Fact]
    public void should_resolve_multiple_indirects()
    {
        var keys = Inventory.Add<SetOfKeys>();

        var orange = Objects.Get<OrangeBox>();
        orange.MoveToLocation();

        var opaque = Objects.Get<OpaqueBox>();
        opaque.MoveToLocation();

        var magenta = Objects.Get<MagentaBox>();
        magenta.MoveToLocation();

        CommandPrompt.FakeInput("opaque box");

        Execute("put keys in box");

        Assert.Contains($"Which do you mean, {orange.DName}, {opaque.DName} or {magenta.DName}?", ConsoleOut);
        Assert.Contains($"You put {keys.DName} into {opaque.DName}.", ConsoleOut);
    }

    [Fact]
    public void should_resolve_object_when_multiple_objects_have_same_adjective()
    {
        // in Colossal Cave "bottled" will find "bottled oil" and "bottled water"
        Execute("purloin bottled oil");

        Assert.DoesNotContain("Which do you mean", ConsoleOut);
        Assert.Contains("[Purloined.]", ConsoleOut);
    }

    [Fact]
    public void should_resolve_object_when_multiple_objects_have_same_adjective_indirect()
    {
        var red = Objects.Get<RedHat>();
        red.MoveToLocation();

        var oil = Objects.Get<OilInTheBottle>();
        oil.MoveToLocation();

        var water = Objects.Get<WaterInTheBottle>();
        water.MoveToLocation();

        var parsed = new Parsed
        {
            VerbToken = "take",
            PossibleRoutines = [Routines.Get<Take>()],
        };

        // in Colossal Cave "bottled" will find "bottled oil" and "bottled water"
        // nonsense sentence but fine for a test
        var tokens = new List<string> { "hat", "from", "bottled", "water" };

        var parser = new Parser();
        parsed = parser.GetObjects(parsed, tokens);

        Assert.DoesNotContain("Which do you mean", ConsoleOut);
        Assert.Single(parsed.Objects);
        Assert.Single(parsed.IndirectObjects);
        Assert.Contains(water, parsed.IndirectObjects);
    }
}
