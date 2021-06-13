using Adventure.Net;

namespace ColossalCave.Things
{
    public abstract class Scenic : Item
    {
        protected Scenic()
        {
            IsScenery = true;
        }
    }
}
