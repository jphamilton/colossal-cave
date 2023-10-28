﻿using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class PlantStickingUp : Object
{
    public override void Initialize()
    {
        Name = "beanstalk";
        Synonyms.Are("plant", "beanstalk", "stalk", "bean", "giant", "tiny", "little", "murmuring", "twelve", "foot", "tall", "bellowing");
        Static = true;
        Absent = true;

        FoundIn<WestEndOfTwoPitRoom, EastEndOfTwoPitRoom>();

        Describe = () =>
        {
            var plant = Get<Plant>();

            if (plant.Height == PlantSize.Tall)
            {
                return "The top of a 12-foot-tall beanstalk is poking out of the west pit.";
            }

            return "There is a huge beanstalk growing out of the west pit up to the hole.";
        };

        Before<Examine>(() =>
        {
            Describe();
            return true;
        });

        Before<Climb>(() =>
        {
            var plant = Get<Plant>();

            if (plant.Height == PlantSize.Huge)
            {
                return Redirect<Climb>(plant, v => v.Expects(plant));
            }

            return false;
        });
    }
}
