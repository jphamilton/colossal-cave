﻿using Adventure.Net;
using ColossalCave.Objects;
using Xunit;

namespace Tests.ObjectTests
{
    public class BrassLanternTests : BaseTestFixture
    {
        private BrassLantern lamp;

        public BrassLanternTests()
        {
            lamp = Objects.Get<BrassLantern>();
            Inventory.Add(lamp);
        }

        [Fact]
        public void should_accept_fresh_batteries()
        {
            var fresh = Objects.Get<FreshBatteries>();
            Inventory.Add(fresh);
            
            lamp.PowerRemaining = 0;
            
            Execute("put batteries in lamp");
            
            Assert.Equal(2500, lamp.PowerRemaining);
            
            Assert.Contains("I'm taking the liberty of replacing the batteries.", ConsoleOut);
            
            var old = Objects.Get<OldBatteries>();
            
            Assert.True(old.InScope);
            Assert.True(fresh.HaveBeenUsed);
        }

        [Fact]
        public void should_reject_old_batteries()
        {
            Inventory.Add(Objects.Get<OldBatteries>());
            
            Execute("put batteries in lamp");
            
            Assert.Contains("Those batteries are dead; they won't do any good at all.", ConsoleOut);            
        }

        [Fact]
        public void cannot_accept_anything_other_than_batteries()
        {
            var bottle = Objects.Get<Bottle>();
            Inventory.Add(bottle);
            
            Execute("put bottle in lamp");

            Assert.Contains("The only thing you might successfully put in the lamp is a fresh pair of batteries.", ConsoleOut);
        }

        [Fact]
        public void should_not_switch_on_if_batteries_are_dead()
        {
            lamp.PowerRemaining = 0;
            
            Execute("turn on lamp");

            Assert.False(lamp.IsOn);

            Assert.Contains("Unfortunately, the batteries seem to be dead.", ConsoleOut);
        }

        [Fact]
        public void can_turn_on_lamp()
        {
            lamp.IsOn = false;

            Assert.False(lamp.HasLight);

            Execute("turn on lamp");

            Assert.True(lamp.IsOn);
            Assert.True(lamp.HasLight);
        }

        [Fact]
        public void can_turn_off_lamp()
        {
            lamp.IsOn = true;

            Execute("turn off lamp");

            Assert.False(lamp.IsOn);
            Assert.False(lamp.HasLight);
        }

        [Fact]
        public void cannot_turn_on_lamp_twice()
        {
            lamp.IsOn = true;
            lamp.HasLight = true;

            Execute("turn on lamp");

            Assert.True(lamp.IsOn);
            Assert.True(lamp.HasLight);
            Assert.Equal("That's already on.", Line(1));
        }

        [Fact]
        public void cannot_turn_off_lamp_twice()
        {
            lamp.IsOn = false;
            lamp.HasLight = false;

            var result = Execute("turn off lamp");

            Assert.False(lamp.IsOn);
            Assert.False(lamp.HasLight);
            Assert.Equal("That's already off.", Line(1));
        }

    }
}
