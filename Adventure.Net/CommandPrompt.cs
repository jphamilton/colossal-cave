using System.IO;

namespace Adventure.Net
{
    public class CommandPrompt
    {
        protected TextWriter output;
        protected TextReader input;
        
        public CommandPrompt(TextWriter output, TextReader input)
        {
            this.output = output;
            this.input = input;
        }
        
        public string GetInput()
        {
            output.Write("\n> ");
            string command = input.ReadLine().Trim();
            return command;
        }
    }
}