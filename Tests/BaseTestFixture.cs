using Adventure.Net;
using ColossalCave;
using ColossalCave.Places;
using ColossalCave.Things;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests;

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
    private List<string> list;
    private Parser parser;

    public BaseTestFixture()
    {
        parser = new Parser();
        fakeConsole = new StringBuilder();
        Context.Story = new ColossalCaveStory();
        Output.Initialize(new StringWriter(fakeConsole), new TestFormatter());
        CommandPrompt.Initialize(new StringWriter(), new StringReader(""));
        Context.Story.Initialize();
        Context.Story.Location = Room<InsideBuilding>();
        Inventory.Clear();
        fakeConsole.Clear();
    }

    public void Dispose()
    {
        list = null;
        Context.Current = null;
        fakeConsole.Clear();
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

            output = fakeConsole.ToString();

            return output;
        }
    }

    protected void Print(string message)
    {
        Context.Current.Print(message);
    }

    protected ParserResult Parse(string input)
    {
        return parser.Parse(input);
    }

    protected CommandResult Execute(string input)
    {
        var result = Parse(input);

        var command = result.CommandHandler();

        return command.Run();
    }

    protected string Line(int number)
    {
        list ??= ConsoleOut.Split(Environment.NewLine).ToList();

        if (list.Count > number)
        {
            return list[number - 1];
        }

        return null;
    }

    protected string Line1 => Line(1);
    protected string Line2 => Line(2);
    protected string Line3 => Line(3);
}
