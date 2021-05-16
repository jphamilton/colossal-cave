using System;

namespace Adventure.Net
{
    public class StoryController 
    {
        public int Moves { get; private set; }

        public StoryController(IStory story) 
        {
            Console.Title = story.Story;
            Output.Initialize(Console.Out);
            Context.CommandPrompt = new CommandPrompt(Console.Out, Console.In);
            Context.Story = story ?? throw new ArgumentNullException("story");
            Context.Parser = new Parser();
        }

        public void Run()
        {
            Context.Story.Initialize();

            //Library.Banner();
            Library.MovePlayerTo(Context.Story.Location);

            while (!Context.Story.IsDone)
            {
                Context.Parser.Parse(Context.CommandPrompt.GetInput());
                Moves++;
                Library.RunDaemons();
            }

        }

        
    }
}