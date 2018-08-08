using Adventure.Net;

namespace ColossalCave.MyObjects
{
    public class Shards : Object
    {
        public override void Initialize()
        {
            Name = "some worthless shards of pottery";
            Synonyms.Are("pottery", "shards", "remains", "vase", "worthless");
            Description = "They look to be the remains of what was once a beautiful vase. " +
                          "I guess some oaf must have dropped it.";
            InitialDescription = "The floor is littered with worthless shards of pottery.";
            // has multitude
        }
    }

}
