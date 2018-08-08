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
            if (story == null) throw new ArgumentNullException("story");
            if (output == null) throw new ArgumentNullException("output");
            if (commandPrompt == null) throw new ArgumentNullException("commandPrompt");

            Context.Output = output;
            Context.CommandPrompt = commandPrompt;
            Context.Story = story;
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