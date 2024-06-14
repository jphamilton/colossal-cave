using Adventure.Net.Extensions;
using Adventure.Net.Things;
using System;
using System.Linq;

namespace Adventure.Net;

public class StoryController
{
    public StoryController(IStory story)
    {
        Console.Title = story.Name;
        Output.Initialize(Console.Out, new WordWrap());
        CommandPrompt.Initialize(Console.Out, Console.In);
        Context.Story = story ?? throw new ArgumentNullException(nameof(story));
    }

    public void Run()
    {
        var story = Context.Story;

        story.Initialize();

        var parser = new Parser();

        while (!story.IsDone)
        {
            var room = Player.Location;
            bool wasLit = CurrentRoom.IsLit();

            var result = parser.Parse(CommandPrompt.GetInput());
            
            if (result.Handled)
            {
                continue;
            }

            if (result.Error.HasValue())
            {
                Output.Print(result.Error);
                continue;
            }
            else
            {
                var handler = result.CommandHandler();
                handler.Run();
            }

            Look(room, wasLit);

            if (!result.Verb.GameVerb)
            {
                story.Moves++;
            }

            RunDaemons();
        }

    }

    private static void Look(Room originalRoom, bool wasLit)
    {
        // player was in darkness, did not move, and light source was turned on
        if (!wasLit && CurrentRoom.IsLit() && Player.Location == originalRoom)
        {
            CurrentRoom.Look(true);
            return;
        }

        // player had light, did not move, and light source was turned off
        if (wasLit && Player.Location == originalRoom && !CurrentRoom.IsLit())
        {
            Output.Print("\r\nIt is now pitch dark in here!");
        }
    }
    
    private static void RunDaemons()
    {
        foreach (var obj in Objects.All.Where(x => x.Daemon != null && x.DaemonRunning))
        {
            obj.Daemon();
        }
    }

}