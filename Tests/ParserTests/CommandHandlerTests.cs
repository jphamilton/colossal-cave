using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Things;
using ColossalCave.Places;
using System.Collections.Generic;
using Xunit;

namespace Tests.ParserTests
{
    public class CommandHandlerTests : BaseTestFixture
    {
        [Fact]
        public void should_not_run()
        {
            var result = Execute("");
            Assert.Equal(Messages.DoNotUnderstand, result.Error);
            Assert.Contains(Messages.DoNotUnderstand, ConsoleOut);
        }

        [Fact]
        public void should_not_recognize_verb()
        {
            Execute("snark");
            // will be one or the depending if this was a partial command response or not
            Assert.Contains(Line1, new List<string> { Messages.VerbNotRecognized, Messages.CantSeeObject });
        }

        [Fact]
        public void go_house()
        {
            Context.Story.Location = Room<EndOfRoad>();

            Execute("go house");
            
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
            Execute("turn lamp down");
            
            Assert.Contains(Messages.PartialUnderstanding(Verb.Get<Turn>(), Objects.Get<BrassLantern>()), ConsoleOut);
        }

        [Fact]
        public void turn_to_switch_redirect()
        {
            var lamp = Objects.Get<BrassLantern>();
            Assert.False(lamp.On);

            Execute("turn lamp on");

            Assert.True(lamp.On);
        }

        [Fact]
        public void switch_to_switchon_lamp_redirect()
        {
            var lamp = Objects.Get<BrassLantern>();
            Assert.False(lamp.On);

            Execute("switch lamp on");

            var x = ConsoleOut;

            Assert.True(lamp.On);
        }

        [Fact]
        public void before_enter_shared()
        {
            Execute("enter spring");

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

            Assert.True(CurrentRoom.Has<BrassLantern>());

            Execute("take lamp");

            Assert.True(Inventory.Contains<BrassLantern>());
            Assert.False(CurrentRoom.Has<BrassLantern>());

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

            Assert.True(CurrentRoom.Has<BrassLantern>());

            Execute("take lamp");

            Assert.False(after);
            Assert.False(Inventory.Contains<BrassLantern>());
            Assert.True(CurrentRoom.Has<BrassLantern>());

            Assert.Contains(message, ConsoleOut);
        }

        [Fact]
        public void multiple_object_should_show_message_for_each_object()
        {
            Execute("take all");

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
            
            lamp.Remove();

            Inventory.Add(lamp);

            Execute("drop all");

            Assert.Contains($"(the {lamp.Name})", ConsoleOut);
            Assert.Contains("Dropped.", ConsoleOut);
            
        }
    }
}
