using ColossalCave.MyObjects;
using Adventure.Net;
using NUnit.Framework;

namespace Advent.Tests.ObjectTests
{
    [TestFixture]
    public class LittleBirdTests : AdventTestFixture
    {
        [Test]
        public void cannot_release_bird_when_its_not_in_the_cage()
        {
            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("release bird");
            Assert.AreEqual("The bird is not caged now.", results[0]);
        }

        [Test]
        public void bird_should_be_unhappy()
        {
            var bird = Objects.Get<LittleBird>();
            var cage = Objects.Get<WickerCage>();

            cage.Add(bird);
            cage.IsOpen = false;
            Inventory.Add(cage);

            var results = parser.Parse("examine bird");
            Assert.AreEqual("The little bird looks unhappy in the cage.", results[0]);
        }

        [Test]
        public void bird_should_be_happy()
        {
            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);
            var results = parser.Parse("examine bird");
            Assert.AreEqual("The cheerful little bird is sitting here singing.", results[0]);
        }

        [Test]
        public void bird_should_be_released_when_cage_dropped()
        {
            var bird = Objects.Get<LittleBird>();
            var cage = Objects.Get<WickerCage>();

            cage.Add(bird);
            cage.IsOpen = false;
            Inventory.Add(cage);

            Assert.IsTrue(cage.Contains<LittleBird>());

            var results = parser.Parse("drop bird");
            Assert.AreEqual("(The bird is released from the cage.)", results[0]);

            Assert.IsTrue(Location.Objects.Contains(bird));
            Assert.IsFalse(cage.Contains<LittleBird>());

        }

        [Test]
        public void bird_should_be_released_when_removed()
        {
            var bird = Objects.Get<LittleBird>();
            var cage = Objects.Get<WickerCage>();

            cage.Add(bird);
            cage.IsOpen = false;
            Inventory.Add(cage);

            Assert.IsTrue(cage.Contains<LittleBird>());

            var results = parser.Parse("remove bird");
            Assert.AreEqual("(The bird is released from the cage.)", results[0]);

            Assert.IsTrue(Location.Objects.Contains(bird));
            Assert.IsFalse(cage.Contains<LittleBird>());

        }

        [Test]
        public void cannot_take_the_bird_again()
        {
            var bird = Objects.Get<LittleBird>();
            var cage = Objects.Get<WickerCage>();

            cage.Add(bird);
            cage.IsOpen = false;
            Inventory.Add(cage);

            Assert.IsTrue(cage.Contains<LittleBird>());

            var results = parser.Parse("take bird");
            Assert.AreEqual("You already have the little bird.", results[0]);
            Assert.AreEqual("If you take it out of the cage it will likely fly away from you.", results[1]);

            Assert.IsTrue(cage.Contains<LittleBird>());
        }

        [Test]
        public void cannot_catch_the_bird_again()
        {
            var bird = Objects.Get<LittleBird>();
            var cage = Objects.Get<WickerCage>();

            cage.Add(bird);
            cage.IsOpen = false;
            Inventory.Add(cage);

            Assert.IsTrue(cage.Contains<LittleBird>());

            var results = parser.Parse("catch bird");
            Assert.AreEqual("You already have the little bird.", results[0]);
            Assert.AreEqual("If you take it out of the cage it will likely fly away from you.", results[1]);

            Assert.IsTrue(cage.Contains<LittleBird>());
        }

        [Test]
        public void cannot_catch_bird_without_cage()
        {
            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);
            var results = parser.Parse("catch bird");
            Assert.AreEqual("You can catch the bird, but you cannot carry it.", results[0]);
            Assert.IsFalse(Inventory.Contains<LittleBird>());
        }

        [Test]
        public void cannot_catch_bird_if_holding_black_rod()
        {
            var blackRod = Objects.Get<BlackRod>();
            Inventory.Add(blackRod);

            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("catch bird");
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("The bird was unafraid when you entered,", results[0]);
            Assert.AreEqual("but as you approach it becomes disturbed and you cannot catch it.", results[1]);

        }

        [Test]
        public void should_take_bird()
        {
            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("take bird");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("You catch the bird in the wicker cage.", results[0]);
            Assert.IsFalse(Location.Objects.Contains(bird));
            Assert.IsTrue(cage.Contains<LittleBird>());
            Assert.IsTrue(bird.InInventory);
        }

        [Test]
        public void cannot_release_bird_if_its_not_in_the_cage()
        {
            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("release bird");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("The bird is not caged now.", results[0]);

        }

        [Test]
        public void bird_should_kill_snake()
        {
            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Objects.Get<LittleBird>();
            cage.Add(bird);

            var snake = Objects.Get<Snake>();
            Location.Objects.Add(snake);

            var results = parser.Parse("release bird");

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("The little bird attacks the green snake,", results[0]);
            Assert.AreEqual("and in an astounding flurry drives the snake away.", results[1]);
            Assert.IsFalse(Location.Objects.Contains(snake));
            Assert.IsFalse(cage.Contains<LittleBird>());
            Assert.IsTrue(Location.Contains<LittleBird>());
        }

        [Test]
        public void dragon_should_kill_bird()
        {
            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Objects.Get<LittleBird>();
            cage.Add(bird);

            var dragon = Objects.Get<Dragon>();
            Location.Objects.Add(dragon);

            var results = parser.Parse("release bird");
            Assert.AreEqual("The little bird attacks the green dragon,", results[0]);
            Assert.AreEqual("and in an astounding flurry gets burnt to a cinder.", results[1]);
            Assert.AreEqual("The ashes blow away.", results[2]);

            Assert.IsFalse(bird.AtLocation);
            Assert.IsFalse(bird.InInventory);
            Assert.IsFalse(bird.InScope);
            
        }

        [Test]
        public void only_the_wicker_cage_can_hold_the_bird()
        {
            var oven = new Oven();
            oven.Initialize();
            Objects.Add(oven);
            Location.Objects.Add(oven);

            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("put bird into oven");
            Assert.AreEqual("Don't put the poor bird in the oven!", results[0]);

        }

        [Test]
        public void can_insert_bird_into_cage()
        {
            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("put bird into cage");
            Assert.AreEqual("You catch the bird in the wicker cage.", results[0]);
        }

        [Test]
        public void can_catch_bird()
        {
            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);

            var results = parser.Parse("catch bird");
            Assert.AreEqual("You catch the bird in the wicker cage.", results[0]);
        }

        [Test]
        public void leave_the_poor_bird_alone()
        {
            var cage = Objects.Get<WickerCage>();
            Inventory.Add(cage);

            var bird = Objects.Get<LittleBird>();
            cage.Add(bird);

            var results = parser.Parse("attack bird");
            Assert.AreEqual("Oh, leave the poor unhappy bird alone.", results[0]);
        }

        [Test]
        public void the_bird_is_dead()
        {
            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);
            var results = parser.Parse("attack bird");
            Assert.AreEqual("The little bird is now dead. Its body disappears.", results[0]);
        }

        [Test]
        public void cannot_ask_bird_about_stuff()
        {
            var bird = Objects.Get<LittleBird>();
            Location.Objects.Add(bird);

            var snake = Objects.Get<Snake>();
            Location.Objects.Add(snake);

            var results = parser.Parse("ask bird about snake");
            Assert.AreEqual("Cheep! Chirp!", results[0]);
        }

        [Test]
        public void implement_life_give()
        {
            Assert.Fail();
        }
    }
}
