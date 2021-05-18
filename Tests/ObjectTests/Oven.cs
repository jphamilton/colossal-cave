using Adventure.Net;

namespace Tests.ObjectTests
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