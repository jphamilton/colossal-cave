using Adventure.Net;

namespace ColossalCave.MyObjects
{
    public class VelvetPillow : Object
    {
        public override void Initialize()
        {
            Name = "velvet pillow";
            Synonyms.Are("velvet", "pillow", "small");
            Description = "It's just a small velvet pillow.";
            InitialDescription = "A small velvet pillow lies on the floor.";

  //          Object  -> velvet_pillow "velvet pillow"
  //with  name 'pillow' 'velvet' 'small',
  //      description "It's just a small velvet pillow.",
  //      initial "A small velvet pillow lies on the floor.";

        }
    }
}
