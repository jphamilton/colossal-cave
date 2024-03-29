﻿using Adventure.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;

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

    public IList<string> Messages
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

    public CommandContext(ParserResult parsed)
    {
        Parsed = parsed;
        IsMulti = parsed.Objects.Count > 1 && (parsed.Verb.Multi || parsed.Verb.MultiHeld);
        Second = parsed.IndirectObject;

        OrderedOutput = new List<IList<string>>();
        OutputStack = new Stack<CommandOutput>();
    }

    public bool IsMulti { get; }

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

    public ParserResult Parsed { get; }

    // this changes as the command handler iterates the objects
    public Object Noun { get; set; }

    // this never changes.
    public Object Second { get; }


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
            OrderedOutput.Add(popped.Messages);
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

    public void Print(string message, CommandState? state = null)
    {
        if (OutputStack.Count == 0)
        {
            Output.Print(message);
            return;
        }

        var messages = OutputStack.Peek();
        var process = state ?? State;

        message = message.Capitalize();

        switch (process)
        {
            case CommandState.Before:
                messages.BeforeOutput.Add(IsMulti ? $"{Noun.Name}: {message}" : message);
                break;

            case CommandState.During:
                messages.DuringOutput.Add(IsMulti ? $"{Noun.Name}: {message}" : message);
                break;

            case CommandState.After:
                messages.DuringOutput.Clear();
                messages.AfterOutput.Add(message);
                break;
        }
    }


}
