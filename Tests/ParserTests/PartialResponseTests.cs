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
            var result = Execute("take");

            Assert.Equal("What do you want to take?", result.Output.Single());

            result = Execute("bottle");

            Assert.Equal("Taken.", result.Output.Single());

            Assert.Contains(Objects.Get<Bottle>(), Inventory.Items);
        }

        [Fact]
        public void should_allow_multiple_partial_commands()
        {
            var result = Execute("take");

            Assert.Equal("What do you want to take?", result.Output.Single());

            result = Execute("take");

            Assert.Equal("What do you want to take?", result.Output.Single());

            result = Execute("take");

            Assert.Equal("What do you want to take?", result.Output.Single());

        }

        [Fact]
        public void should_allow_multiple_partial_responses()
        {
            var result = Execute("take");

            Assert.Equal("What do you want to take?", result.Output.Single());

            result = Execute("bottle");

            Assert.Equal("Taken.", result.Output.Single());

            Assert.Contains(Objects.Get<Bottle>(), Inventory.Items);

            result = Execute("bottle");

            Assert.Equal(Messages.VerbNotRecognized, result.Output.Single());
        }

        [Fact]
        public void should_distinguish_between_one_word_commands_and_partial_command()
        {
            Execute("look");

            Assert.DoesNotContain("What do you want to", ConsoleOut);

            Clear();

            Execute("west");

            Assert.DoesNotContain("What do you want to", ConsoleOut);

            Clear();

            Execute("enter");

            Assert.DoesNotContain("What do you want to", ConsoleOut);

        }
    }
}
