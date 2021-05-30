using Adventure.Net;
using ColossalCave.Objects;
using ColossalCave.Places;
using Xunit;

namespace Tests.Verbs
{

    public class CloseTests : BaseTestFixture
    {
        [Fact]
        public void cannot_close_things_that_are_not_meant_to_be_closed()
        {
            Location = Room<InsideBuilding>();
            var result = Execute("close bottle");
            Assert.Equal("That's not something you can close.", Line(1));
        }

        [Fact]
        public void cannot_close_things_that_are_already_closed()
        {
            Location = Room<OutsideGrate>();
            var result = Execute("close grate");
            Assert.Equal("That's already closed.", Line(1));
        }

        [Fact]
        public void can_close()
        {
            Location = Room<OutsideGrate>();
            Door grate = Room<Grate>() as Door;
            grate.IsOpen = true;
            var result = Execute("close grate");
            Assert.Equal("You close the steel grate.", Line(1));
        }

        [Fact]
        public void can_close_up()
        {
            Location = Room<OutsideGrate>();
            
            Door grate = Room<Grate>() as Door;
            
            grate.IsOpen = true;
            
            Execute("close up grate");
            
            Assert.Equal("You close the steel grate.", Line(1));
        }

        [Fact]
        public void can_close_off_lamp()
        {
            Location = Room<InsideBuilding>();

            var lamp = Objects.Get<BrassLantern>();
            lamp.IsOn = true;

            Execute("close off lamp");

            Assert.Equal("You switch the brass lantern off.", Line(1));
        }

        [Fact]
        public void close_with_invalid_preposition()
        {
            Location = Room<InsideBuilding>();

            Execute("close on lamp");

            Assert.Equal(Messages.CantSeeObject, Line(1));
        }
    }
}
