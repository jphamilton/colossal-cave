using Adventure.Net.Things;
using Adventure.Net;
using ColossalCave.Places;
using ColossalCave;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System;
using Xunit;

using Object = Adventure.Net.Object;
using ColossalCave.Things;

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
    private static List<Adventure.Net.Object> _testObjects = [];

    private ParserResult previous;

    public BaseTestFixture()
    {
        parser = new Parser();
        fakeConsole = new StringBuilder();
        Context.Story = new ColossalCaveStory();
        Output.Initialize(new StringWriter(fakeConsole), new TestFormatter());
        CommandPrompt.Initialize(new StringWriter(), new StringReader(""));
        Context.Story.Initialize();

        Global.State.CavesClosed = false;
        Global.State.CanyonFrom = null;
        Global.State.TreasuresFound = 0;
        Global.State.Deaths = 0;

        //if (_testObjects == null)
        //{
        _testObjects = [];
        Assembly ax = typeof(BaseTestFixture).Assembly;

        var objectType = typeof(Adventure.Net.Object);
        var objectTypes = ax.GetTypes().Where(x => x.IsSubclassOf(objectType)).ToList();

        foreach (var type in objectTypes)
        {
            if (Activator.CreateInstance(type) is Adventure.Net.Object obj)
            {
                obj.Initialize();
                _testObjects.Add(obj);
                Dictionary.AddObject(obj);
            }
        }

        Player.Location = Room<InsideBuilding>();
        Inventory.Clear();
        fakeConsole.Clear();
        AddTestObjects();
    }

    private void AddTestObjects()
    {
        foreach(var obj in _testObjects)
        {
            Objects.All.Add(obj);
            Dictionary.AddObject(obj);
        }
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
            return Player.Location;
        }
        set
        {
            Player.Location = value;
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

    protected bool Print(string message)
    {
        Context.Current.Print(message);
        return true;
    }

    protected CommandResult Execute(string input)
    {
        var index = 0;

        while(true)
        {
            bool died = false;

            index++;

            if (index > 10)
            {
                throw new Exception("Infinite loop detected");
            }

            var room = Player.Location;
            bool wasLit = CurrentRoom.IsLit();

            try
            {
                var pr = parser.Parse(input, previous);

                if (pr.IsError)
                {
                    Output.Print(pr.Error);
                    break;
                }

                if (pr.IsPartial)
                {
                    Output.Print(pr.PartialMessage);
                    input = CommandPrompt.GetInput();
                    previous = pr;
                    continue;
                }

                if (!pr.IsHandled)
                {
                    Output.Print(pr.Aside);

                    var command = new Command(pr);
                    var run = command.Run();
                    Output.Print(run.Output);

                    CurrentRoom.Look(room, wasLit);

                    previous = null;

                    return run;
                }
            }
            catch (DeathException)
            {
                var dead = Routines.GetDeathRoutine();

                while (Context.Current.PopState())
                {
                }

                List<string> messages = [.. Context.Current.Messages];

                Context.Current = null;

                Output.Print(messages);

                if (!dead.Handler(null, null))
                {
                    break;
                }
            }
            
            previous = null;

            break;
        }
        
        return null;
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

    protected T Inv<T>() where T : Object
    {
        return (T)Inventory.Add<T>();
    }

    protected T Get<T>() where T : Object
    {
        return Objects.Get<T>();
    }
}
