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
            Context.Story.Initialize();

            MovePlayer.To(Context.Story.Location);

            while (!Context.Story.IsDone)
            {
                bool wasLit = CurrentRoom.IsLit();

                // TODO: clean this up
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

                if (!wasLit && CurrentRoom.IsLit())
                {
                    CurrentRoom.Look(true);
                }

                Context.Story.Moves++;
                
                RunDaemons();
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