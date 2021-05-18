using Adventure.Net;

namespace ColossalCave.Places
{
    public abstract class Scenic : Item
    {
        protected Scenic()
        {
            IsScenery = true;
        }
    }
}
