﻿using Adventure.Net;
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

        ClearOutput();

        Execute("bottle");
        
        Assert.Contains(Messages.VerbNotRecognized, ConsoleOut);
    }

    [Fact]
    public void should_allow_full_command_from_partial()
    {
        CommandPrompt.FakeInput("take the bottle");

        Execute("take");

        Assert.Equal("What do you want to take?", Line1);
        Assert.Equal("Taken.", Line2);
        Assert.Contains(Objects.Get<Bottle>(), Inventory.Items);
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

        fresh.MoveToLocation();

        Inventory.Add(lamp);

        lamp.PowerRemaining = 0;

        CommandPrompt.FakeInput("lamp");

        Execute("put batteries in");

        Assert.Contains($"What do you want to put {fresh.DName} in?", ConsoleOut);
        Assert.Contains("I'm taking the liberty of replacing the batteries.", ConsoleOut);

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
        var keys = Objects.Get<SetOfKeys>();
        var lamp = Objects.Get<BrassLantern>();
        var fresh = Objects.Get<FreshBatteries>();
        
        fresh.MoveToLocation();
        
        // multiple items prevents implicit actions
        Inventory.Add(lamp);
        Inventory.Add(keys);

        lamp.PowerRemaining = 0;

        CommandPrompt.FakeInput("lamp");

        Execute("put batteries");

        Assert.Contains($"What do you want to put {fresh.DName} in?", ConsoleOut);
        Assert.Contains("I'm taking the liberty of replacing the batteries.", ConsoleOut);

        Assert.Equal(2500, lamp.PowerRemaining);

        var old = Objects.Get<OldBatteries>();

        Assert.True(old.InScope);
        Assert.True(fresh.HaveBeenUsed);
    }

    
}
