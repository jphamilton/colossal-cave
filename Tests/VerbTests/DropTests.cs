using Adventure.Net;
using ColossalCave.Places;
using Xunit;

namespace Tests.VerbTests
{
    public class DropTests : BaseTestFixture
    {
        [Fact]
        public void should_handle_drop()
        {
            var bottle = Objects.Get<Bottle>();
            CurrentRoom.Objects.Remove(bottle);
            Inventory.Add(bottle);

            // not holding bottle, but bottle is in the room
            var result = parser.Parse("drop bottle");

            var command = result.CommandHandler();
            command.Run();

            Assert.Contains($"Dropped.", ConsoleOut);
        }

        [Fact]
        public void should_handle_multiheld_not_held_but_in_room()
        {
            var bottle = Objects.Get<Bottle>();

            // not holding bottle, but bottle is in the room
            var result = parser.Parse("drop bottle");

            var command = result.CommandHandler();
            command.Run();

            Assert.Contains($"The {bottle.Name} is already here.", ConsoleOut);
        }

        [Fact]
        public void shoud_handle_drop_all__with_no_inventory()
        {
            var result = parser.Parse("drop all");
            var command = result.CommandHandler();

            command.Run();
            Assert.Contains("You aren't carrying anything.", ConsoleOut);
        }
    }
}
