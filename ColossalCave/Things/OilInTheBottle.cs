using Adventure.Net;

namespace ColossalCave.Things;

public class OilInTheBottle : Object
{
    public override void Initialize()
    {
        Name = "bottled oil";
        Synonyms.Are("oil", "bottled", "lubricant", "grease");
        IndefiniteArticle = "some";
        Description = "It looks like ordinary oil to me";

        //        before [;
        //          Drink:
        //            <<Drink Oil>>;
        //        ];

    }
}

