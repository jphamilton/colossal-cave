﻿using Adventure.Net;
using ColossalCave.Places;
using ColossalCave.Things;
using Tests.ObjectTests;
using Xunit;

namespace Tests.ParserTests;

public class ImplicitTakeTests : BaseTestFixture
{
    [Fact]
    public void implicit_take_should_trigger_before_after_routines()
    {
        var oven = new Oven();
        oven.Initialize();

        var rocks = new BagOfRocks();
        rocks.Initialize();

        Objects.Add(oven, CurrentRoom.Location);
        Objects.Add(rocks, CurrentRoom.Location);
        
        Inventory.Add(oven);

        var result = Execute("put rocks in oven");

        Assert.Contains("The bag is too heavy.", ConsoleOut);
        Assert.False(Inventory.Contains(rocks));
    }

    [Fact]
    public void should_implicitly_take_indirect_object()
    {
        Location = Rooms.Get<OutsideGrate>();

        // keys are on the ground
        var keys = Objects.Get<SetOfKeys>();
        keys.MoveToLocation();

        var parser = new Parser();

        // key should be implicitly taken
        var result = parser.Parse("unlock grate with key");

        Assert.Equal(keys, result.ImplicitTake);
    }

    [Fact]
    public void should_implicitly_take_item_out_of_container()
    {
        // not sure how to fix this test yet and keep many others passing

        Location = Rooms.Get<Y2>();
        Location.Light = true;

        var cage = Objects.Get<WickerCage>();
        Inventory.Add(cage);

        var bottle = Objects.Get<Bottle>();

        cage.Add(bottle);

        Execute("put bottle on rock");

        Assert.Contains("(first taking the small bottle out of the wicker cage)", ConsoleOut);
    }

    [Fact]
    public void should_print_messages_properly()
    {
        Location = Rooms.Get<HallOfMists>();
        Location.Light = true;

        var axe = Objects.Get<Axe>();
        axe.MoveToLocation();

        var dwarf = Objects.Get<Dwarf>();
        dwarf.MoveToLocation();

        Execute("throw axe at dwarf");

        Assert.Contains("(first taking the dwarvish axe)", ConsoleOut);
        Assert.Contains("(first taking the dwarvish axe)", Line1);
    }
}
