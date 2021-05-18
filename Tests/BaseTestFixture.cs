using Adventure.Net;
using ColossalCave;
using ColossalCave.Places;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Tests
{
    [Collection("Sequential")]
    public abstract class BaseTestFixture
    {
        protected CommandLineParser parser;
        protected StringBuilder fakeConsole;
        private string _output;
        private CommandResult _currentCommandResult;

        public BaseTestFixture()
        {
            parser = new CommandLineParser();
            fakeConsole = new StringBuilder();
            Context.Story = new ColossalCaveStory();
            Output.Initialize(new StringWriter(fakeConsole));
            CommandPrompt.Initialize(new StringWriter(), new StringReader(""));
            Context.Story.Initialize();
            Context.Story.Location = Rooms.Get<InsideBuilding>();
            Inventory.Clear();
            fakeConsole.Clear();
        }

        protected Room Room
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
                if (!string.IsNullOrEmpty(_output))
                {
                    return _output;
                }

                _output = fakeConsole.ToString().Replace(Environment.NewLine, "");
                return _output;
            }
        }

        protected void Print(string message)
        {
            Context.Current.Print(message);
        }

        protected CommandResult Execute(string input)
        {
            var result = parser.Parse(input);
            var command = result.CommandHandler();
            _currentCommandResult = command.Run();
            return _currentCommandResult;
        }

        protected string Line(int number)
        {
            return _currentCommandResult != null ? _currentCommandResult.Output[number - 1] : null;
        }

       
    }
}
