using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
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
