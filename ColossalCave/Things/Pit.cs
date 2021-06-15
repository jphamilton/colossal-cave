using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Things
{
    public class Pit : Scenic
    {
        public override void Initialize()
        {
            Name = "pit";
            Synonyms.Are("pit", "thirty", "foot", "thirty-foot");
            Description = "You'll have to climb down to find out anything more...";

            Before<Climb>(() =>
            {
                CurrentRoom.Location.DOWN();
                return true;
            });
        }
    }

}


