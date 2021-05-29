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
        private CommandResult _commandResult;

        public BaseTestFixture()
        {
            parser = new CommandLineParser();
            fakeConsole = new StringBuilder();
            Context.Story = new ColossalCaveStory();
            Output.Initialize(new StringWriter(fakeConsole));
            CommandPrompt.Initialize(new StringWriter(), new StringReader(""));
            Context.Story.Initialize();
            Context.Story.Location = Room<InsideBuilding>();
            Inventory.Clear();
            fakeConsole.Clear(); ;
        }

        protected void Clear()
        {
            _output = null;
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
            _commandResult = command.Run();
            return _commandResult;
        }

        protected string Line(int number)
        {
            if (_commandResult != null && 
                _commandResult.Output.Count >= number )
            {
                return _commandResult.Output[number - 1];
            }

            return null;
        }

       
    }
}
