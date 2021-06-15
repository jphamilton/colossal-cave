using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public enum PlantSize
    {
        Tiny,
        Tall,
        Huge
    }

    public class Plant : Item
    {
        public PlantSize Height { get; set; }

        public override void Initialize()
        {
            Name = "plant";
            Synonyms.Are("plant", "beanstalk", "stalk", "bean", "giant", "tiny", "little", "murmuring", "twelve", "foot", "tall", "bellowing");
            Height = PlantSize.Tiny;
            
            Describe = () =>
            {
                if (Height == PlantSize.Tiny)
                {
                    return "There is a tiny little plant in the pit, murmuring \"Water, water, ...\"";
                }
                else if (Height == PlantSize.Tall)
                {
                    return "There is a 12-foot-tall beanstalk stretching up out of the pit, bellowing \"Water!! Water!!\"";
                }

                return "There is a gigantic beanstalk stretching all the way up to the hole.";
            };

            Before<Climb>(() =>
            {

                if (Height == PlantSize.Tiny)
                {
                    Print("It's just a little plant!");
                    return true;
                }
                else if (Height == PlantSize.Tall)
                {
                    Print("You have climbed up the plant and out of the pit.\r\n");
                    MovePlayer.To<WestEndOfTwoPitRoom>();
                    return true;
                }

                Print("You clamber up the plant and scurry through the hole at the top.\r\n");
                //MovePlayer.To<NarrowCorridor>();
                return true;
            });

            Before<Take>(() =>
            {
                Print("The plant has exceptionally deep roots and cannot be pulled free.");
                return true;
            });

            Before<Water>(() =>
            {
                var bottle = Get<Bottle>();
                
                if (bottle.IsEmpty)
                {
                    Print("You have nothing to water the plant with.");
                    return true;
                }

                if (bottle.Contains<OilInTheBottle>())
                {
                    Remove<OilInTheBottle>();
                    Print("The plant indignantly shakes the oil off its leaves and asks, \"Water?\"");
                    return true;
                }

                Remove<WaterInTheBottle>();

                var plantStickingUp = Get<PlantStickingUp>();

                switch(Height++)
                {
                    case PlantSize.Tiny:
                        Print("The plant spurts into furious growth for a few seconds.\r\n\r\n");
                        //give PlantStickingUp ~absent;
                        break;

                    case PlantSize.Tall:
                        Print("The plant grows explosively, almost filling the bottom of the pit.\r\n\r\n");
                        break;

                    case PlantSize.Huge:
                        Print("You've over-watered the plant! It's shriveling up! It's, it's...\r\n\r\n");
                        
                        //give PlantStickingUp absent;
                        //remove PlantStickingUp;
                        Height = PlantSize.Tiny;
                        break;
                }

                // <<Examine self>>;

                return true;
            });

            //Before Oil:
            //<< Water self >>;

            //Before Examine:
            //self.describe();
            //rtrue;
        }
    }
}
