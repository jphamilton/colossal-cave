using Adventure.Net;
using ColossalCave.Objects;
using System.Linq;
using Xunit;

namespace Tests.ParserTests
{
    // Tests partial responses
    //
    // > take
    // What do you want to take?
    //
    // > bottle
    // Taken.
    public class PartialResponseTests : BaseTestFixture
    {
        [Fact]
        public void should_handle_partial_response()
        {
            CommandPrompt.FakeInput("bottle");

            Execute("take");

            Assert.Contains("What do you want to take?", ConsoleOut);
            Assert.Contains("Taken.", ConsoleOut);
            Assert.Contains(Objects.Get<Bottle>(), Inventory.Items);
        }

        [Fact]
        public void should_allow_multiple_partial_commands()
        {
            CommandPrompt.FakeInput("take");

            var result = Execute("take");

            Assert.Equal("What do you want to take?", Line(1));
            Assert.Equal("What do you want to take?", Line(2));
        }

        [Fact]
        public void should_allow_multiple_partial_responses()
        {

            CommandPrompt.FakeInput("bottle");

            Execute("take");

            Assert.Contains("What do you want to take?", ConsoleOut);
            Assert.Contains("Taken.", ConsoleOut);
            Assert.Contains(Objects.Get<Bottle>(), Inventory.Items);

            var result = Execute("bottle");

            Assert.Equal(Messages.VerbNotRecognized, result.Output.Single());
        }

        [Fact]
        public void should_distinguish_between_one_word_commands_and_partial_command()
        {
            Execute("look");

            Assert.DoesNotContain("What do you want to", ConsoleOut);

            ClearOutput();
            Execute("west");

            Assert.DoesNotContain("What do you want to", ConsoleOut);

            ClearOutput();
            Execute("enter");

            Assert.DoesNotContain("What do you want to", ConsoleOut);

            ClearOutput();
            Execute("i");

            Assert.DoesNotContain("What do you want to", ConsoleOut);
        }
    }
}
