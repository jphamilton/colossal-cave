using Adventure.Net;
using Xunit;

namespace Tests.ParserTests;

public class MultipleMatchedItemsTests : BaseTestFixture
{
    [Fact]
    public void should_handle_two_objects_with_same_name()
    {
        var room = Context.Story.Location;

        var red = new RedHat();
        red.Initialize();

        var black = new BlackHat();
        black.Initialize();

        Objects.Add(red, room);
        Objects.Add(black, room);

        CommandPrompt.FakeInput("red");

        Execute("take hat");

        Assert.Contains("Which do you mean, the red hat or the black hat?", ConsoleOut);
        Assert.Contains("Taken.", ConsoleOut);
        Assert.Contains(red, Inventory.Items);
    }

    [Fact]
    public void should_handle_more_than_two_objects_with_same_name()
    {
        var room = Context.Story.Location;

        var red = new RedHat();
        red.Initialize();

        var black = new BlackHat();
        black.Initialize();

        var white = new WhiteHat();
        white.Initialize();

        Objects.Add(red, room);
        Objects.Add(black, room);
        Objects.Add(white, room);

        CommandPrompt.FakeInput("white");

        Execute("take hat");

        Assert.Contains("Which do you mean, the red hat, the black hat or the white hat?", ConsoleOut);
        Assert.Contains("Taken.", ConsoleOut);
        Assert.Contains(white, Inventory.Items);
    }

    [Fact]
    public void should_handle_two_objects_with_same_name_in_except_clause()
    {
        var room = Context.Story.Location;

        var red = new RedHat();
        red.Initialize();

        var black = new BlackHat();
        black.Initialize();

        var white = new WhiteHat();
        white.Initialize();

        Objects.Add(red, room);
        Objects.Add(black, room);
        Objects.Add(white, room);

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
        var room = Context.Story.Location;

        var red = new RedHat();
        red.Initialize();

        var black = new BlackHat();
        black.Initialize();

        var white = new WhiteHat();
        white.Initialize();

        Objects.Add(red, room);
        Objects.Add(black, room);
        Objects.Add(white, room);

        // this should bail out of the except clause handler
        CommandPrompt.FakeInput("take white hat");

        Execute("take all except hat");

        Assert.DoesNotContain("white hat: Taken.", ConsoleOut);
        Assert.DoesNotContain(white, Inventory.Items);
        Assert.Contains(red, Inventory.Items);
        Assert.Contains(black, Inventory.Items);
    }

    [Fact]
    public void should_handle_a_specific_multiple_matched_item_entered_using_two_words()
    {
        var room = Context.Story.Location;

        var red = new RedHat();
        red.Initialize();

        var black = new BlackHat();
        black.Initialize();

        var white = new WhiteHat();
        white.Initialize();

        Objects.Add(red, room);
        Objects.Add(black, room);
        Objects.Add(white, room);

        Execute("take white hat");

        Assert.Contains("Taken.", ConsoleOut);
    }

    [Fact]
    public void should_handle_bad_command_reentry_in_except_clause_that_has_multiple_matching_items()
    {
        var room = Context.Story.Location;

        var red = new RedHat();
        red.Initialize();

        var black = new BlackHat();
        black.Initialize();

        var white = new WhiteHat();
        white.Initialize();

        Objects.Add(red, room);
        Objects.Add(black, room);
        Objects.Add(white, room);

        CommandPrompt.FakeInput("donkey");

        Execute("take all except hat");

        Assert.Equal(Messages.CantSeeObject, Line1);
    }

    [Fact]
    public void should_repeatedly_try_to_resolve_multiple_objects()
    {
        var room = Context.Story.Location;

        var red = new RedHat();
        red.Initialize();

        var black = new BlackHat();
        black.Initialize();

        var white = new WhiteHat();
        white.Initialize();

        Objects.Add(red, room);
        Objects.Add(black, room);
        Objects.Add(white, room);

        CommandPrompt.FakeInput("hat");

        Execute("take hat");

        Assert.Contains("Which do you mean, the red hat, the black hat or the white hat?", Line1);
        Assert.Contains("Which do you mean, the red hat, the black hat or the white hat?", Line2);
    }

    [Fact]
    public void should_resolve_object_when_multiple_objects_have_same_adjective()
    {
        // in Colossal Cave "bottled" will find "bottled oil" and "bottled water"
        Execute("purloin bottled oil");

        Assert.DoesNotContain("Which do you mean", ConsoleOut);
        Assert.Contains("[Purloined.]", ConsoleOut);
    }
}
