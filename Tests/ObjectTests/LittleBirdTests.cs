using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.ObjectTests;


public class LittleBirdTests : BaseTestFixture
{
    [Fact]
    public void cannot_release_bird_when_its_not_in_the_cage()
    {
        var bird = Objects.Get<LittleBird>();

        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("release bird");

        Assert.Equal("The bird is not caged now.", Line1);
    }

    [Fact]
    public void bird_should_be_unhappy()
    {
        var bird = Objects.Get<LittleBird>();
        var cage = Objects.Get<WickerCage>();

        cage.Add(bird);
        cage.Open = false;
        Inventory.Add(cage);

        Execute("examine bird");

        Assert.Equal("The little bird looks unhappy in the cage.", Line1);
    }

    [Fact]
    public void bird_should_be_happy()
    {
        var bird = Objects.Get<LittleBird>();
        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("examine bird");

        Assert.Equal("The cheerful little bird is sitting here singing.", Line1);
    }

    [Fact]
    public void bird_should_be_released_when_cage_dropped()
    {
        var bird = Objects.Get<LittleBird>();
        var cage = Objects.Get<WickerCage>();

        cage.Add(bird);
        cage.Open = false;
        Inventory.Add(cage);

        Assert.True(cage.Contains<LittleBird>());

        var result = Execute("drop bird");

        Assert.Equal("(The bird is released from the cage.)", Line1);
        Assert.Equal("The little bird flies free.", Line2);

        Assert.True(CurrentRoom.Has<LittleBird>());
        Assert.False(cage.Contains<LittleBird>());

    }

    [Fact]
    public void bird_should_be_released_when_removed()
    {
        var bird = Objects.Get<LittleBird>();
        var cage = Objects.Get<WickerCage>();

        cage.Add(bird);
        cage.Open = false;
        Inventory.Add(cage);

        Assert.True(cage.Contains<LittleBird>());

        Execute("remove bird");

        Assert.Equal("(The bird is released from the cage.)", Line1);
        Assert.Equal("The little bird flies free.", Line2);

        Assert.True(CurrentRoom.Has<LittleBird>());
        Assert.False(cage.Contains<LittleBird>());

    }

    [Fact]
    public void cannot_take_the_bird_again()
    {
        var bird = Objects.Get<LittleBird>();
        var cage = Objects.Get<WickerCage>();

        cage.Add(bird);
        cage.Open = false;

        Inventory.Add(cage);

        Assert.True(cage.Contains<LittleBird>());

        Execute("take bird");

        Assert.Equal("You already have the little bird.", Line1);
        Assert.Equal("If you take it out of the cage it will likely fly away from you.", Line2);

        Assert.True(cage.Contains<LittleBird>());
    }

    [Fact]
    public void cannot_catch_the_bird_again()
    {
        var bird = Objects.Get<LittleBird>();
        var cage = Objects.Get<WickerCage>();

        cage.Add(bird);
        cage.Open = false;
        Inventory.Add(cage);

        Assert.True(cage.Contains<LittleBird>());

        var result = Execute("catch bird");

        Assert.Equal("You already have the little bird.", Line1);
        Assert.Equal("If you take it out of the cage it will likely fly away from you.", Line2);
        Assert.True(cage.Contains<LittleBird>());
    }

    [Fact]
    public void cannot_catch_bird_without_cage()
    {
        var bird = Objects.Get<LittleBird>();
        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("catch bird");

        Assert.Equal("You can catch the bird, but you cannot carry it.", Line1);
        Assert.False(Inventory.Contains<LittleBird>());
    }

    [Fact]
    public void cannot_catch_bird_if_holding_black_rod()
    {
        var blackRod = Objects.Get<BlackRod>();
        Inventory.Add(blackRod);

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("catch bird");

        Assert.Equal("The bird was unafraid when you entered, but as you approach it becomes disturbed and you cannot catch it.", Line1);

    }

    [Fact]
    public void should_take_bird()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("take bird");

        Assert.Equal("You catch the bird in the wicker cage.", Line1);
        Assert.False(CurrentRoom.Has<LittleBird>());
        Assert.True(cage.Contains<LittleBird>());
        Assert.True(bird.InInventory);
    }

    [Fact]
    public void cannot_release_bird_if_its_not_in_the_cage()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("release bird");

        Assert.Equal("The bird is not caged now.", Line1);

    }

    [Fact]
    public void bird_should_kill_snake()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        cage.Add(bird);

        var snake = Objects.Get<Snake>();
        ObjectMap.MoveObject(snake, CurrentRoom.Location);

        Execute("release bird");

        Assert.Equal("The little bird attacks the green snake, and in an astounding flurry drives the snake away.", Line1);
        Assert.False(CurrentRoom.Has<Snake>());
        Assert.False(cage.Contains<LittleBird>());
        Assert.True(CurrentRoom.Has<LittleBird>());
    }

    [Fact]
    public void dragon_should_kill_bird()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        cage.Add(bird);

        var dragon = Objects.Get<Dragon>();
        ObjectMap.MoveObject(dragon, CurrentRoom.Location);

        Execute("release bird");

        Assert.Contains("The little bird attacks the green dragon,", ConsoleOut);
        Assert.Contains("and in an astounding flurry gets burnt to a cinder.", ConsoleOut);
        Assert.Contains("The ashes blow away.", ConsoleOut);

        Assert.False(CurrentRoom.Has<LittleBird>());
        Assert.False(bird.InInventory);
        Assert.False(bird.InScope);

    }

    [Fact]
    public void only_the_wicker_cage_can_hold_the_bird()
    {
        var oven = new Oven();
        oven.Initialize();

        Objects.Add(oven, CurrentRoom.Location);

        var bird = Objects.Get<LittleBird>();
        //Objects.Add for testing
        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("put bird into oven");

        var x = ConsoleOut;

        Assert.Equal("Don't put the poor bird in the oven!", Line1);

    }

    [Fact]
    public void can_insert_bird_into_cage()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("put bird into cage");

        Assert.Equal("You catch the bird in the wicker cage.", Line1);
    }

    [Fact]
    public void can_catch_bird()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();

        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("catch bird");

        Assert.Equal("You catch the bird in the wicker cage.", Line1);
    }

    [Fact]
    public void leave_the_poor_bird_alone()
    {
        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bird = Objects.Get<LittleBird>();
        cage.Add(bird);

        Execute("attack bird");

        Assert.Equal("Oh, leave the poor unhappy bird alone.", Line1);
    }

    [Fact]
    public void the_bird_is_dead()
    {
        var bird = Objects.Get<LittleBird>();

        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        Execute("attack bird");

        Assert.Equal("The little bird is now dead. Its body disappears.", Line1);
    }

    [Fact]
    public void cannot_ask_bird_about_stuff()
    {
        var bird = Objects.Get<LittleBird>();
        ObjectMap.MoveObject(bird, CurrentRoom.Location);

        var snake = Objects.Get<Snake>();

        ObjectMap.MoveObject(snake, CurrentRoom.Location);

        Execute("ask bird about snake");
        var x = ConsoleOut;

        Assert.Equal("Cheep! Chirp!", Line1);
    }


}
