using Adventure.Net;
using ColossalCave.Things;
using Xunit;

namespace Tests.Verbs
{

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
            Assert.Equal("You catch the bird in the wicker cage.", Line1);
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
            Assert.Equal("You catch the bird in the wicker cage.", Line1);
        }

        [Fact]
        public void what_do_you_want_to_put_the_bottle_on()
        {
            CommandPrompt.FakeInput("keys");

            var bottle = Objects.Get<Bottle>();
            bottle.MoveToLocation();

            var keys = Objects.Get<SetOfKeys>();
            keys.MoveToLocation();

            Execute("put bottle on");
            Assert.Equal("You need to be holding the small bottle before you can put it on top of something else.", Line1);
        }

        [Fact]
        public void just_put_object_not_present()
        {
            CommandPrompt.FakeInput("bird in cage");

            Execute("put");
            Assert.Equal("You can't see any such thing.", Line1);
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
            Assert.Equal("You catch the bird in the wicker cage.", Line1);
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
            Assert.Equal("You catch the bird in the wicker cage.", Line1);
        }

        [Fact]
        public void start_with_bird_should_actually_do_implicit_put()
        {
            // bird has a Before<Insert> action defined so the parser should implicitly call it:
            //
            // > put
            //   What do you want to put?
            // > bird
            //   (in the little bird)
            //   Don't put the poor bird in the little bird!
            //
            // However, the Before<Insert> action on the bird should be rewritten to disallow this specific case.

            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Objects.Get<LittleBird>();
            bird.MoveToLocation();

            CommandPrompt.FakeInput("bird");

            Execute("put");
            var x = ConsoleOut;
            Assert.Equal("(in the little bird)", Line1);
            Assert.Equal("Don't put the poor bird in the little bird!", Line2);
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
            Assert.Equal("You already have the little bird.", Line1);
            Assert.Equal("If you take it out of the cage it will likely fly away from you.", Line2);
        }


        [Fact]
        public void just_put_all_except()
        {
            CommandPrompt.FakeInput("bird");

            Execute("put all except");

            Assert.Equal("What do you want to put?", Line1);
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
    }
}