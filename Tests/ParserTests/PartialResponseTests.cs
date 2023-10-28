using Adventure.Net;
using ColossalCave.Things;
using System.Linq;
using Xunit;

namespace Tests.ParserTests;

// Tests partial responses
//
// > take
// What do you want to take?
//
// > bottle
// Taken.
public class PartialResponseTests : BaseTestFixture
{
    [Fact]
    public void should_handle_partial_response()
    {
        CommandPrompt.FakeInput("bottle");

        Execute("take");

        Assert.Contains("What do you want to take?", ConsoleOut);
        Assert.Contains("Taken.", ConsoleOut);
        Assert.Contains(Objects.Get<Bottle>(), Inventory.Items);
    }

    [Fact]
    public void should_allow_multiple_partial_commands()
    {
        CommandPrompt.FakeInput("take");

        var result = Execute("take");

        Assert.Equal("What do you want to take?", Line1);
        Assert.Equal("What do you want to take?", Line2);
    }

    [Fact]
    public void should_allow_multiple_partial_responses()
    {

        CommandPrompt.FakeInput("bottle");

        Execute("take");

        Assert.Contains("What do you want to take?", ConsoleOut);
        Assert.Contains("Taken.", ConsoleOut);
        Assert.Contains(Objects.Get<Bottle>(), Inventory.Items);

        var result = Execute("bottle");

        Assert.Equal(Messages.VerbNotRecognized, result.Output.Single());
    }

    [Fact]
    public void should_distinguish_between_one_word_commands_and_partial_command()
    {
        Execute("look");

        Assert.DoesNotContain("What do you want to", ConsoleOut);

        ClearOutput();
        Execute("west");

        Assert.DoesNotContain("What do you want to", ConsoleOut);

        ClearOutput();
        Execute("enter");

        Assert.DoesNotContain("What do you want to", ConsoleOut);

        ClearOutput();
        Execute("i");

        Assert.DoesNotContain("What do you want to", ConsoleOut);
    }

    [Fact]
    public void should_handle_partial_command_with_preposition()
    {
        var lamp = Objects.Get<BrassLantern>();
        var fresh = Objects.Get<FreshBatteries>();
        Inventory.Add(fresh);

        lamp.PowerRemaining = 0;

        CommandPrompt.FakeInput("lamp");

        Execute("put batteries in");

        var x = ConsoleOut;

        Assert.Contains($"What do you want to put the {fresh} in?", Line1);
        Assert.Contains("I'm taking the liberty of replacing the batteries.", Line2);

        Assert.Equal(2500, lamp.PowerRemaining);


        var old = Objects.Get<OldBatteries>();

        Assert.True(old.InScope);
        Assert.True(fresh.HaveBeenUsed);
    }

    [Fact]
    public void put_all_in()
    {
        Execute("put all in");
        Assert.Contains($"What do you want to put those things in?", Line1);
    }

    [Fact]
    public void should_use_default_proposition_in_partial_command()
    {
        var lamp = Objects.Get<BrassLantern>();
        var fresh = Objects.Get<FreshBatteries>();
        Inventory.Add(fresh);

        lamp.PowerRemaining = 0;

        CommandPrompt.FakeInput("lamp");

        Execute("put batteries");

        var x = ConsoleOut;

        Assert.Contains($"What do you want to put the {fresh} in?", Line1);
        Assert.Contains("I'm taking the liberty of replacing the batteries.", Line2);

        Assert.Equal(2500, lamp.PowerRemaining);

        var old = Objects.Get<OldBatteries>();

        Assert.True(old.InScope);
        Assert.True(fresh.HaveBeenUsed);
    }

    [Fact]
    public void should_handle_this_ridiculous_partial_command_sequence()
    {
        CommandPrompt.FakeInput("stream\rbottle");
        Execute("put");
        Assert.Contains("The bottle is now full of water.", ConsoleOut);
    }
}
