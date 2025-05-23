using Adventure.Net.Things;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System;

namespace Adventure.Net;

[ExcludeFromCodeCoverage]
public class StoryController
{
    public StoryController(Story story)
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
        ParserResult previous = null;

        while (!story.IsDone)
        {
            var room = Player.Location;
            bool wasLit = CurrentRoom.IsLit();

            var pr = parser.Parse(CommandPrompt.GetInput(), previous);

            // implicit actions can trigger things like death
            if (story.IsDone)
            {
                break;
            }

            if (pr.IsError || pr.IsPartial)
            {
                Output.Print(pr.Error); // will print partial message if it exists here
                previous = pr;
                continue;
            }

            previous = null;

            if (!pr.IsHandled)
            {
                Output.Print(pr.Aside);

                var command = new Command(pr);
                var run = command.Run();
                
                if (Context.Story.IsDone)
                {
                    break;
                }

                Output.Print(run.Output);

                CurrentRoom.Look(room, wasLit);
            }

            if (!pr.Routine.IsGameVerb)
            {
                story.Moves++;
            }

            RunDaemons();
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey(true);
    }
    
    private static void RunDaemons()
    {
        foreach (var obj in Objects.All.Where(x => x.Daemon != null && x.DaemonRunning))
        {
            obj.Daemon();
        }
    }

}