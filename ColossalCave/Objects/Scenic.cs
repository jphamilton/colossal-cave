using Adventure.Net;

namespace ColossalCave.Objects
{
    public abstract class Scenic : Item
    {
        protected Scenic()
        {
            IsScenery = true;
        }
    }
}
