using Adventure.Net.Extensions;
using Adventure.Net.Things;
using System;
using System.Linq;

namespace Adventure.Net;

public class StoryController
{
    public StoryController(IStory story)
    {
        Console.Title = story.Story;

        Output.Initialize(Console.Out, new WordWrap());
        CommandPrompt.Initialize(Console.Out, Console.In);

        Context.Story = story ?? throw new ArgumentNullException("story");
    }

    public void Run()
    {
        var story = Context.Story;

        story.Initialize();

        MovePlayer.To(Player.Location);

        var parser = new Parser();

        while (!story.IsDone)
        {
            var room = CurrentRoom.Location;
            bool wasLit = CurrentRoom.IsLit();

            var result = parser.Parse(CommandPrompt.GetInput());
            
            if (result.Handled)
            {
                continue;
            }

            if (result.Error.HasValue())
            {
                Output.Print(result.Error);
            }
            else
            {
                var handler = result.CommandHandler();
                handler.Run();
            }

            if (!wasLit && CurrentRoom.IsLit() && CurrentRoom.Location == room)
            {
                CurrentRoom.Look(true);
            }

            if (wasLit && !CurrentRoom.IsLit())
            {
                Output.Print("\r\nIt is now pitch dark in here!");
            }

            if (!result.Verb.GameVerb)
            {
                story.Moves++;
            }

            RunDaemons();
        }

    }

    public static void RunDaemons()
    {
        foreach (var obj in Objects.All.Where(x => x.Daemon != null && x.DaemonRunning).ToList())
        {
            obj.Daemon();
        }
    }

}