using Adventure.Net;
using ColossalCave.Objects;
using Xunit;

namespace Tests.VerbTests
{
    public class EatTests : BaseTestFixture
    {
        [Fact]
        public void cannot_eat_inedible_things()
        {
            Inventory.Add(Objects.Get<Bottle>());
            Execute("eat bottle");
            Assert.Equal("That's plainly inedible.", Line(1));
        }

        [Fact]
        public void should_eat_edible_things_in_inventory()
        {
            var tastyFood = Objects.Get<TastyFood>();

            Inventory.Add(tastyFood);
            Execute("eat food");
            Assert.Equal("Delicious!", Line(1));
        }

    }
}
