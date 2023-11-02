using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.Verbs;


public class PutTests : BaseTestFixture
{
    [Fact]
    public void what_do_you_want_to_put_the_bottle_in()
    {

        var bird = Objects.Get<LittleBird>();
        bird.MoveToLocation();

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        CommandPrompt.FakeInput("cage");

        Execute("put bird");
        Assert.Contains("You catch the bird in the wicker cage.", ConsoleOut);
    }

    [Fact]
    public void restate_command_after_incomplete_question()
    {
        CommandPrompt.FakeInput("put bird in cage");

        var bird = Objects.Get<LittleBird>();
        bird.MoveToLocation();

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        Execute("put bird");
        Assert.Contains("You catch the bird in the wicker cage.", ConsoleOut);
    }

    [Fact]
    public void what_do_you_want_to_put_the_bottle_on()
    {
        var bottle = Objects.Get<Bottle>();
        bottle.MoveToLocation();

        var keys = Objects.Get<SetOfKeys>();
        keys.MoveToLocation();

        Execute("put bottle on");
        Assert.Contains("What do you want to put the small bottle on?", ConsoleOut);
        ClearOutput();
        
        Execute("keys");

        Assert.Contains("You need to be holding the small bottle before you can put it on top of something else.", ConsoleOut);
    }

    [Fact]
    public void just_put_object_not_present()
    {
        CommandPrompt.FakeInput("bird in cage");

        Execute("put");
        Assert.Contains("You can't see any such thing.", ConsoleOut);
    }

    [Fact]
    public void just_put_object_present()
    {
        CommandPrompt.FakeInput("bird in cage");

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        bird.MoveToLocation();

        Execute("put");
        Assert.Contains("You catch the bird in the wicker cage.", ConsoleOut);
    }

    [Fact]
    public void start_with_bird()
    {
        CommandPrompt.FakeInput("bird\ncage");

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        bird.MoveToLocation();

        Execute("put");
        Assert.Contains("You catch the bird in the wicker cage.", ConsoleOut);
    }

    [Fact]
    public void what_do_you_want_to_put_the_bird_in()
    {
        // inventory = bird in cage
        // put
        // what do you want put?
        // bird
        // what do you want to put the little bird in?
        // cage
        // You already have the little bird. If you take it out of the cage it will likely fly away from you.

        CommandPrompt.FakeInput("bird\ncage");

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        cage.Add(bird);

        Execute("put");
        Assert.Contains("You already have the little bird.", ConsoleOut);
        Assert.Contains("If you take it out of the cage it will likely fly away from you.", ConsoleOut);
    }


    [Fact]
    public void just_put_all_except()
    {
        CommandPrompt.FakeInput("bird");

        Execute("put all except");

        Assert.Contains("What do you want to put those things in?", ConsoleOut);
        Assert.Equal(Messages.CantSeeObject, Line2);
    }

    [Fact]
    public void should_put_multiple_held_into_held_container()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var keys = Objects.Get<SetOfKeys>();
        Inventory.Add(keys);

        var bottle = Objects.Get<Bottle>();
        Inventory.Add(bottle);

        var food = Objects.Get<TastyFood>();
        Inventory.Add(food);

        Execute("put all in cage");

        Assert.Contains($"{keys.Name}: Done.", ConsoleOut);
        Assert.Contains($"{bottle.Name}: Done.", ConsoleOut);
        Assert.Contains($"{food.Name}: Done.", ConsoleOut);
    }

    [Fact]
    public void should_implicit_take()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        ClearOutput();

        Execute("put bottle in cage");

        // can fix this test by changing method in Put to use [Held] attribute,
        // but this breaks other things
        // public bool Expects([Held] Object obj, Preposition.In @in, Object indirect)

        // When [Held] attribute is used, you can't "put bird in cage"

        Assert.Contains("(first taking the small bottle)", ConsoleOut);
        Assert.Contains("You put the small bottle into the wicker cage.", ConsoleOut);
    }
}