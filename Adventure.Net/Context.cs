using System.Collections.Generic;

namespace Adventure.Net
{
    public static class Context
    {
        public static IStory Story { get; set; }

        public static Stack<CommandContext> Stack { get; } = new Stack<CommandContext>();

        public static CommandContext Current
        {
            get { return Stack.Peek(); }
        }
    }
}
