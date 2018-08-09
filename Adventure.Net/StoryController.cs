using System;

namespace Adventure.Net
{
    public class StoryController 
    {
        public int Moves { get; private set; }

        public StoryController(IStory story) : this(story, new Output(Console.Out), new CommandPrompt(Console.Out, Console.In))
        {
            
        }

        public StoryController(IStory story, Output output, CommandPrompt commandPrompt)
        {
            Context.Output = output ?? throw new ArgumentNullException("output");
            Context.CommandPrompt = commandPrompt ?? throw new ArgumentNullException("commandPrompt");
            Context.Story = story ?? throw new ArgumentNullException("story");
            Context.Parser = new Parser();

        }

        public void Run()
        {
            var L = new Library();
            
            Context.Story.Initialize();
            L.Banner();
            L.MovePlayerTo(Context.Story.Location);

            while (!Context.Story.IsDone)
            {
                Context.Parser.Parse(Context.CommandPrompt.GetInput());
                Moves++;
                L.RunDaemons();
            }

        }

        
    }
}