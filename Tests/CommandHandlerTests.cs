using Adventure.Net;
using Adventure.Net.Verbs;
using ColossalCave.Objects;
using ColossalCave.Places;
using Xunit;

namespace Tests
{
    public class CommandHandlerTests : BaseTestFixture
    {
        [Fact]
        public void should_not_run()
        {
            var result = parser.Parse("");
            Assert.Equal(Messages.DoNotUnderstand, result.Error);
            
            var command = result.CommandHandler();
            command.Run();

            Assert.Contains(Messages.DoNotUnderstand, ConsoleOut);
        }

        [Fact]
        public void should_not_recognize_verb()
        {
            var result = parser.Parse("snark");
            
            var command = result.CommandHandler();
            command.Run();

            Assert.Contains(Messages.VerbNotRecognized, ConsoleOut);
        }

        [Fact]
        public void go_house()
        {
            Context.Story.Location = Room<EndOfRoad>();

            var result = parser.Parse("go house");

            var command = result.CommandHandler();
            command.Run();

            Assert.Equal(Room<InsideBuilding>(), Context.Story.Location);
        }

        [Fact]
        public void i_only_understood_you_as_far_as_wanting_to()
        {
            // invalid command: instead of "switch lamp on" we try to "switch lamp down"
            Execute("switch lamp down");
            Assert.Contains(Messages.PartialUnderstanding(Verb.Get<Switch>(), Objects.Get<BrassLantern>()), ConsoleOut);
        }

        [Fact]
        public void i_only_understood_you_as_far_as_wanting_to_2()
        {
            var result = parser.Parse("turn lamp down");

            var command = result.CommandHandler();
            command.Run();

            Assert.Contains(Messages.PartialUnderstanding(Verb.Get<Turn>(), Objects.Get<BrassLantern>()), ConsoleOut);
        }

        [Fact]
        public void turn_to_switch_redirect()
        {
            var result = parser.Parse("turn lamp on");

            var lamp = Objects.Get<BrassLantern>();
            Assert.False(lamp.IsOn);

            var command = result.CommandHandler();
            command.Run();

            Assert.True(lamp.IsOn);
        }

        [Fact]
        public void switch_to_switchon_lamp_redirect()
        {
            var result = parser.Parse("switch lamp on");

            var lamp = Objects.Get<BrassLantern>();
            Assert.False(lamp.IsOn);

            var command = result.CommandHandler();
            command.Run();

            Assert.True(lamp.IsOn);
        }

        [Fact]
        public void before_enter_shared()
        {
            var result = parser.Parse("enter spring");

            var command = result.CommandHandler();
            command.Run();

            // no exception means this passed
        }

        [Fact]
        public void should_run_before_after_routines_from_expects()
        {
            var lamp = Objects.Get<BrassLantern>();
            var before = false;
            var after = false;

            lamp.Before<Take>(() =>
            {
                before = true;
                return false; // not handled
            });

            lamp.After<Take>(() =>
            {
                after = true;
            });

            Assert.True(Location.Contains<BrassLantern>());

            var result = parser.Parse("take lamp");

            var command = result.CommandHandler();
            command.Run();

            Assert.True(Inventory.Contains<BrassLantern>());
            Assert.False(Location.Contains<BrassLantern>());

            Assert.True(before);
            Assert.True(after);
        }

        [Fact]
        public void before_routine_blocks_continuation()
        {
            var lamp = Objects.Get<BrassLantern>();
            var message = "The lamp is glued to the floor.";
            var after = false;

            lamp.Before<Take>(() =>
            {
                Print(message);
                return true; // not handled
            });

            lamp.After<Take>(() =>
            {
                after = true;
            });

            Assert.True(Location.Contains<BrassLantern>());

            var result = parser.Parse("take lamp");

            var command = result.CommandHandler();
            command.Run();

            Assert.False(after);
            Assert.False(Inventory.Contains<BrassLantern>());
            Assert.True(Location.Contains<BrassLantern>());

            Assert.Contains(message, ConsoleOut);
        }

        [Fact]
        public void multiple_object_should_show_message_for_each_object()
        {
            var result = parser.Parse("take all");

            var command = result.CommandHandler();
            command.Run();

            Assert.Contains("set of keys: Taken.", ConsoleOut);
            Assert.Contains("tasty food: Taken.", ConsoleOut);
            Assert.Contains("brass lantern: Taken.", ConsoleOut);
            Assert.Contains("small bottle: Taken.", ConsoleOut);
            Assert.Contains("stream: The bottle is now full of water.", ConsoleOut);
            Assert.Contains("well house: That's hardly portable.", ConsoleOut);
            Assert.Contains("pair of 1 foot diameter sewer pipes: That's hardly portable.", ConsoleOut);

        }

        [Fact]
        public void handle_multiheld_with_one_item_in_inventory()
        {
            var lamp = Objects.Get<BrassLantern>();
            CurrentRoom.Objects.Remove(lamp);
            Inventory.Add(lamp);

            var result = parser.Parse("drop all");

            var command = result.CommandHandler();
            command.Run();

            Assert.Contains($"(the {lamp.Name})", ConsoleOut);
            Assert.Contains("Dropped.", ConsoleOut);
            
        }
    }
}
