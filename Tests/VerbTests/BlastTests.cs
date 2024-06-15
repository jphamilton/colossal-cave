using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class BlastTests : BaseTestFixture
{
    [Fact]
    public void should_not_blast()
    {
        Execute("blast");
        Assert.Contains("Frustrating, isn't it?", ConsoleOut);
    }

    [Fact]
    public void should_blast()
    {
        Location = Room<SwEnd>();
        var blackMarkRod = Objects.Get<BlackMarkRod>();
        blackMarkRod.MoveTo<NeEnd>();

        Execute("blast");
        Assert.Contains("cheering band of friendly elves", ConsoleOut);
    }

    [Fact]
    public void should_blast_but_also_kill_you()
    {
        Location = Room<NeEnd>();
        var blackMarkRod = Objects.Get<BlackMarkRod>();
        blackMarkRod.MoveTo<SwEnd>();

        CommandPrompt.FakeInput("no");

        Execute("blast");
        
        Assert.Contains("destroying everything in its path, including you!", ConsoleOut);
    }

    [Fact]
    public void should_blast_but_also_kill_you_in_a_different_way()
    {
        Location = Room<SwEnd>();
        var blackMarkRod = Objects.Get<BlackMarkRod>();
        blackMarkRod.MoveTo<SwEnd>();

        CommandPrompt.FakeInput("no");

        Execute("blast");

        Assert.Contains("splashed across the walls of the room", ConsoleOut);
    }

    [Fact]
    public void should_and_revive()
    {
        Location = Room<SwEnd>();
        var blackMarkRod = Objects.Get<BlackMarkRod>();
        blackMarkRod.MoveTo<SwEnd>();

        CommandPrompt.FakeInput("y");

        Execute("blast");

        Assert.Contains("splashed across the walls of the room", ConsoleOut);
    }

    [Fact]
    public void blast_a_donkey()
    {
        Objects.Get<Donkey>().MoveToLocation();
        Inventory.Add<BlackMarkRod>();
        Execute("blast donkey with dynamite");
        Assert.Contains("Been eating those funny brownies again?", ConsoleOut);
    }

    [Fact]
    public void cannot_just_blast_with_anything()
    {
        Objects.Get<Donkey>().MoveToLocation();
        Inventory.Add<Bottle>();
        Execute("blast donkey with bottle");
        Assert.Contains("Blasting requires dynamite.", ConsoleOut);
    }

    [Fact]
    public void cannot_just_blast_with_anything_2()
    {
        Objects.Get<Donkey>().MoveToLocation();
        Inventory.Add<JeweledTrident>();
        Execute("blast donkey with trident");
        Assert.Contains("Blasting requires dynamite.", ConsoleOut);
    }
}
