using System.Collections.Generic;
using System.Linq;
using System;

namespace Adventure.Net;

public class CommandContext : ICommandState
{
    public List<IList<string>> OrderedOutput { get; set; }

    private Stack<CommandOutput> OutputStack { get; }

    public Func<string, string> PrintOverride { get; set; } = (x) => x;

    public CommandContext(ParserResult pr)
    {
        IsMulti = pr.Objects.Count > 1 && pr.Routine.AcceptsManyObjects;
        Second = pr.IndirectObject;
        OrderedOutput = [];
        OutputStack = new Stack<CommandOutput>();
    }

    public bool IsMulti { get; set; }

    public Func<Room> Move { get; set; }

    public IList<string> Messages
    {
        get
        {
            var result = new List<string>();

            var copy = OrderedOutput.ToList();
            copy.Reverse();

            foreach (var list in copy)
            {
                result.AddRange(list);
            }

            return result;
        }
    }

    // this changes as the command handler iterates the objects
    public Object First { get; set; }

    // this never changes.
    public Object Second { get; }

    public ICommandState PushState(CommandOutput commandOutput = null)
    {
        var state = commandOutput ?? new CommandOutput();

        OutputStack.Push(state);

        return state;
    }

    public bool PopState(CommandOutput commandOutput = null)
    {
        if (OutputStack.TryPop(out var popped) && commandOutput == null)
        {
            OrderedOutput.Add(popped.Messages);
            return true;
        }

        return false;
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

    public void Print(string message)
    {
        if (OutputStack.Count == 0)
        {
            Output.Print(message);
            return;
        }

        var after = message;

        var messages = OutputStack.Peek();

        if (IsMulti && State != CommandState.After)
        {
            message = $"{First.Name}: {message}";
        }
        else
        {
            message = PrintOverride(message);
        }

        switch (State)
        {
            case CommandState.Before:
                messages.BeforeOutput.Add(message);
                break;

            case CommandState.During:
                messages.DuringOutput.Add(message);
                break;

            case CommandState.After:
                messages.DuringOutput.Clear();
                messages.AfterOutput.Add(after);
                break;
        }
    }
}
