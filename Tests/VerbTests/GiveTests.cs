using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.VerbTests;

public class GiveTests : BaseTestFixture
{
    [Fact]
    public void should_only_give_animate()
    {
        var food = Inventory.Add<TastyFood>();
        var bottle = Location.Add<Bottle>();

        Execute("give food to bottle");

        Assert.Contains("You can only do that with something animate.", ConsoleOut);
    }

    [Fact]
    public void should_give_animate()
    {
        var bottle = Inventory.Add<Bottle>();
        var donkey = Location.Add<Donkey>();

        Execute("give bottle to donkey");

        Assert.Contains("The donkey doesn't seem interested.", ConsoleOut);
    }

    [Fact]
    public void should_give_food_to_bear()
    {
        var food = Inventory.Add<TastyFood>();
        var bear = Location.Add<Bear>();

        Execute("give food to bear");

        Assert.Contains("The bear eagerly wolfs down your food, after which he seems to calm down considerably and even becomes rather friendly.", ConsoleOut);
        Assert.DoesNotContain(food, Inventory.Items);
    }

    [Fact]
    public void should_give_food_to_bear_with_reverse_syntax()
    {
        var food = Inventory.Add<TastyFood>();
        var bear = Location.Add<Bear>();

        Execute("give bear food");

        Assert.Contains("The bear eagerly wolfs down your food, after which he seems to calm down considerably and even becomes rather friendly.", ConsoleOut);
        Assert.DoesNotContain(food, Inventory.Items);
    }

    [Fact]
    public void should_imply_player_when_not_specified()
    {
        Execute("give");

        Assert.Contains("What do you want to give yourself?", ConsoleOut);
    }

    [Fact]
    public void should_imply_player_when_not_specified_partial()
    {
        var food = Objects.Get<TastyFood>();

        CommandPrompt.FakeInput("food");

        Execute("give");

        Assert.Contains("What do you want to give yourself?", ConsoleOut);
        Assert.Contains($"You juggle {food.DName} for awhile, but don't achieve much.", ConsoleOut);
    }

    [Fact]
    public void should_feed_bear_partial()
    {
        var bear = Location.Add<Bear>();
        var food = Inventory.Add<TastyFood>();
        var lamp = Inventory.Add<BrassLantern>();

        CommandPrompt.FakeInput("food");

        Execute("feed bear");

        Assert.Contains("The bear eagerly wolfs down your food, after which he seems to calm down considerably and even becomes rather friendly.", ConsoleOut);
        Assert.DoesNotContain(food, Inventory.Items);
    }

    [Fact]
    public void should_feed_bear_implicitly()
    {
        var bear = Location.Add<Bear>();
        var food = Inventory.Add<TastyFood>();

        Execute("feed bear");

        Assert.Contains($"({food.DName})", ConsoleOut);
        Assert.Contains("The bear eagerly wolfs down your food, after which he seems to calm down considerably and even becomes rather friendly.", ConsoleOut);
        Assert.DoesNotContain(food, Inventory.Items);
    }
}
