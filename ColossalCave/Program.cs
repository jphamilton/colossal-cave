using Adventure.Net;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ColossalCave;

[ExcludeFromCodeCoverage]
static class Program
{
    static void Main(string[] args)
    {
        Console.Title = "ADVENTURE";
        StoryController controller = new StoryController(new ColossalCaveStory());
        controller.Run();
    }
}
