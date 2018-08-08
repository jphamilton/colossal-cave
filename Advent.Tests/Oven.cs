using Adventure.Net;

namespace Advent.Tests
{
    public class Oven : Container
    {
        public override void Initialize()
        {
            Name = "oven";
            IsOpen = true;
            IsOpenable = true;
            IsTransparent = false;
            Article = "the";
        }
    }
}