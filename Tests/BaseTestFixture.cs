using Adventure.Net;
using ColossalCave;
using ColossalCave.Places;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Tests
{
    public class TestFormatter : IOutputFormatter
    {
        public string Format(string text)
        {
            return text;
        }
    }

    [Collection("Sequential")]
    public abstract class BaseTestFixture : IDisposable
    {
        private StringBuilder fakeConsole;
        private string output;

        protected CommandResult CommandResult { get; set; }
        
        public BaseTestFixture()
        {
            fakeConsole = new StringBuilder();
            Context.Story = new ColossalCaveStory();
            Output.Initialize(new StringWriter(fakeConsole), new TestFormatter());
            CommandPrompt.Initialize(new StringWriter(), new StringReader(""));
            Context.Story.Initialize();
            Context.Story.Location = Room<InsideBuilding>();
            Inventory.Clear();
            fakeConsole.Clear(); ;
        }

        public void Dispose()
        {
            Context.Current = null;
            fakeConsole.Clear();
            CommandResult = null;
            output = null;
            Inventory.Clear();
        }

        protected void ClearOutput()
        {
            output = null;
            fakeConsole.Clear();
        }

        protected Room Room<T>() where T : Room
        {
            return Rooms.Get<T>();
        }

        protected Room Location
        {
            get
            {
                return Context.Story.Location;
            }
            set
            {
                Context.Story.Location = value;
            }
        }

        public string ConsoleOut
        {
            get
            {
                if (!string.IsNullOrEmpty(output))
                {
                    return output;
                }

                output = fakeConsole.ToString().Replace(Environment.NewLine, "");
                return output;
            }
        }

        protected void Print(string message)
        {
            Context.Current.Print(message);
        }

        protected CommandLineParserResult Parse(string input)
        {
            var parser = new CommandLineParser();
            return parser.Parse(input);
        }

        protected CommandResult Execute(string input)
        {
            var result = Parse(input);

            var command = result.CommandHandler();
            
            CommandResult = command.Run();
            
            return CommandResult;
        }

        protected string Line(int number)
        {
            if (CommandResult != null && 
                CommandResult.Output.Count >= number )
            {
                return CommandResult.Output[number - 1];
            }

            return null;
        }

    }
}
