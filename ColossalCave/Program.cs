using Adventure.Net;

namespace ColossalCave
{
    class Program
    {
        static void Main(string[] args)
        {
            StoryController controller = new StoryController(new ColossalCaveStory());
            controller.Run();
        }
    }
}
