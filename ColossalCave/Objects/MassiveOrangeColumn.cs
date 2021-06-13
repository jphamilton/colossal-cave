using Adventure.Net;
using ColossalCave.Verbs;

namespace ColossalCave.Objects
{
    public class MassiveOrangeColumn : Scenic
    {
        public override void Initialize()
        {
            Name = "massive orange column";
            Synonyms.Are("column", "massive", "orange", "big", "huge");
            Description = "It looks like you could climb down it.";

            Before<Climb>(() =>
            {
                CurrentRoom.Location.DOWN();
                return true;
            });

        }
    }
}
