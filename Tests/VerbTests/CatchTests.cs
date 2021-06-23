using ColossalCave.Places;
using Adventure.Net;
using Xunit;
using Adventure.Net.Actions;

namespace Tests.VerbTests
{ 
    public class CatchTests : BaseTestFixture
    {
        [Fact]
        public void cannot_catch_inanimate_objects()
        {
            Location = Room<InsideBuilding>();
            Execute("catch bottle");
            Assert.Equal("You can only do that to something animate.", Line(1));
        }

        [Fact]
        public void cant_catch_this()
        {
            Location = Room<InsideBuilding>();

            var shark = new Shark();
            shark.Initialize();

            Objects.Add(shark, Location);

            Execute("catch shark");
            Assert.Equal("You can't catch that.", Line(1));
        }

        [Fact]
        public void can_catch_this()
        {
            Location = Room<InsideBuilding>();

            var octopus = new Octopus();
            octopus.Initialize();
            
            Objects.Add(octopus, Location);

            Execute("catch octopus");
            Assert.Equal("Yeah right!", Line(1));
        }

    }

    public class Shark : Item
    {
        public override void Initialize()
        {
            Name = "shark";
            Animate = true;
        }
    }

    public class Octopus : Item
    {
        public override void Initialize()
        {
            Name = "octopus";
            Animate = true;

            Before<Catch>(() =>
            {
                Print("Yeah right!");
                return true;
            });
        }
    }

}
