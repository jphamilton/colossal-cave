﻿using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class BlackRod : Object
{
    public override void Initialize()
    {
        Name = "black rod with a rusty star on the end";
        Synonyms.Are("rod", "star", "black", "rusty", "star", "three", "foot", "iron");
        Description = "It's a three foot black rod with a rusty star on an end.";
        InitialDescription = "A three foot black rod with a rusty star on one end lies nearby.";

        FoundIn<DebrisRoom>();

        Before<Wave>(() =>
        {
            var westSideOfFissure = Rooms.Get<WestSideOfFissure>();
            var eastBankOfFissure = Rooms.Get<EastBankOfFissure>();

            if (Player.Location is WestSideOfFissure || Player.Location is EastBankOfFissure)
            {
                if (Global.State.CavesClosed)
                {
                    return Print("Peculiar. Nothing happens.");
                }

                if (CurrentRoom.Has<CrystalBridge>())
                {
                    Room<WestSideOfFissure>().BridgeDisappears();
                    Room<EastBankOfFissure>().BridgeDisappears();
                    Print("The crystal bridge has vanished!");
                }
                else
                {
                    Room<WestSideOfFissure>().BridgeAppears();
                    Room<EastBankOfFissure>().BridgeAppears();
                    Print("A crystal bridge now spans the fissure.");
                }

                return true;
            }

            return Print("Nothing happens.");
        });
    }
}

