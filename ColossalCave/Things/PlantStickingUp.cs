using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class PlantStickingUp : Item
    {
        public override void Initialize()
        {
            Name = "beanstalk";
            Synonyms.Are("plant", "beanstalk", "stalk", "bean", "giant", "tiny", "little", "murmuring", "twelve", "foot", "tall", "bellowing");
            IsStatic = true;
            IsAbsent = true;
            // absent

            Describe = () =>
            {
                var plant = Get<Plant>();
                
                if (plant.Height == PlantSize.Tall)
                {
                    return "The top of a 12-foot-tall beanstalk is poking out of the west pit.";
                }

                return "There is a huge beanstalk growing out of the west pit up to the hole.";
            };

            //Examine:
            //RunRoutines(self, describe);
            //rtrue;

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
}

//        found_in At_West_End_Of_Twopit_Room At_East_End_Of_Twopit_Room,
//  has   absent static;