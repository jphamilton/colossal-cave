using Xunit;

namespace Tests.VerbTests
{
    public class LookTests : BaseTestFixture
    {
        [Fact]
        public void can_look()
        {
            var result = parser.Parse("look");

            var command = result.CommandHandler();
            command.Run();

            Assert.Contains("Inside Building", ConsoleOut);
            Assert.Contains("You are inside a building, a well house for a large spring.", ConsoleOut);
            Assert.Contains("There are some keys on the ground here.", ConsoleOut);
            Assert.Contains("There is tasty food here.", ConsoleOut);
            Assert.Contains("There is a shiny brass lamp nearby.", ConsoleOut);
            Assert.Contains("There is an empty bottle here.", ConsoleOut);
        }
    }
}
