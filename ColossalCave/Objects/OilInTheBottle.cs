using Adventure.Net;

namespace ColossalCave.Places
{
    public class OilInTheBottle : Item
    {
        public override void Initialize()
        {
            Name = "bottled oil";
            Synonyms.Are("oil", "bottled", "lubricant", "grease");
            Article = "some";
            Description = "It looks like ordinary oil to me";

            //        before [;
            //          Drink:
            //            <<Drink Oil>>;
            //        ];

        }
    }
}

