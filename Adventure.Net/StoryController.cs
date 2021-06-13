using Adventure.Net.Extensions;
using System;

namespace Adventure.Net
{
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

            MovePlayer.To(story.Location);

            while (!story.IsDone)
            {
                var room = CurrentRoom.Location;
                bool wasLit = CurrentRoom.IsLit();

                var parser = new CommandLineParser();
                var result = parser.Parse(CommandPrompt.GetInput());
                
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

                story.Moves++;
                
                RunDaemons();

                story.AfterTurn();
            }

        }

        public static void RunDaemons()
        {
            foreach (var obj in Objects.WithRunningDaemons())
            { 
                obj.Daemon();
            }
        }

    }
}