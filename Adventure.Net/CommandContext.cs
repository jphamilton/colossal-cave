using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net
{
    public interface ICommandState
    {
        CommandState State { get; set; }
    }

    public class CommandOutput : ICommandState
    {
        public CommandState State { get; set; }

        public IList<string> BeforeOutput { get; } = new List<string>();
        public IList<string> DuringOutput { get; } = new List<string>();
        public IList<string> AfterOutput { get; } = new List<string>();

        public IList<string> Output
        {
            get
            {
                var result = new List<string>();

                result.AddRange(BeforeOutput);

                // After messages are shown before During messages which,
                // allows objects to add "observations" to mundane actions.
                //
                // An example is the dropping the Ming Vase in Colossal Cave:
                //
                // > drop vase
                // (coming to rest, delicately, on the velvet pillow)
                // Dropped.
                result.AddRange(AfterOutput);

                result.AddRange(DuringOutput);

                return result;
            }
        }
    }

    public class CommandContext : ICommandState
    {
        private List<IList<string>> OrderedOutput { get; }

        private Stack<CommandOutput> OutputStack { get; }

        public CommandContext(CommandLineParserResult parsed)
        {
            Parsed = parsed;
            IsMulti = parsed.Objects.Count > 1 && (parsed.Verb.Multi || parsed.Verb.MultiHeld);
            IndirectObject = parsed.IndirectObject;

            OrderedOutput = new List<IList<string>>();
            OutputStack = new Stack<CommandOutput>();
        }

        public bool IsMulti { get; }

        public IList<string> Output
        {
            get
            {
                var result = new List<string>();

                var copy = OrderedOutput.ToList();
                copy.Reverse();

                foreach(var list in copy)
                {
                    result.AddRange(list);
                }

                return result;
            }
        }

        public CommandLineParserResult Parsed { get; }

        // this changes as the command handler iterates the objects
        public Item CurrentObject { get; set; }

        // this never changes.
        public Item IndirectObject { get; }

       
        public ICommandState PushState(CommandOutput commandOutput = null)
        {
            var state = commandOutput ?? new CommandOutput();
            
            OutputStack.Push(state);

            return state;
        }

        public void PopState(CommandOutput commandOutput = null)
        {
            var popped = OutputStack.Pop();

            if (commandOutput == null)
            {
                OrderedOutput.Add(popped.Output);
            }
        }

        public CommandState State 
        {
            get
            {
                return OutputStack.Peek().State;
            }
            set
            {
                OutputStack.Peek().State = value;
            }
        }

        public bool Partial { get; internal set; }

        public void Print(string message, CommandState? state = null)
        {
            var messages = OutputStack.Peek();
            var process = state ?? State;

            switch (process)
            {
                case CommandState.Before:
                    messages.BeforeOutput.Add(IsMulti ? $"{CurrentObject.Name}: {message}" : message);
                    break;

                case CommandState.During:
                    messages.DuringOutput.Add(IsMulti ? $"{CurrentObject.Name}: {message}" : message);
                    break;

                case CommandState.After:
                    messages.AfterOutput.Add(message);
                    break;
            }
        }

       
    }
}
