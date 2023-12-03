using Adventure.Net.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net;

public abstract class Verb
{
    // ()
    private readonly Dictionary<string, MethodInfo> NoArgs = new();
    // (obj)
    private readonly Dictionary<string, MethodInfo> OneArg = new();
    // (obj, prep)
    private readonly Dictionary<string, MethodInfo> TwoArgs = new();
    // (obj, prep, indirect)
    private readonly Dictionary<string, MethodInfo> ThreeArgs = new();

    // temporary
    public List<Prep> AcceptedPrepositions { get; } = new List<Prep>();

    public void Initialize()
    {
        var methods = GetExpectsMethods();

        if (methods.Any())
        {
            foreach (var method in methods)
            {
                GetExpectsParameters(method);
            }
        }
    }

    // Score is an example of a game verb. When true, command does not increment game turns.
    public bool GameVerb { get; set; }

    public bool InScopeOnly { get; set; } = true;
    
    public MethodInfo GetHandler(string key)
    {
        var parameters = key.Split('.').ToList();
        return GetHandler(parameters);
    }

    public MethodInfo GetHandler(Parameters parameters)
    {
        return GetHandler(parameters.Key);
    }

    private MethodInfo GetHandler(IList<string> parameters)
    {
        string key = string.Join('.', parameters);
        bool checkReverse = false;

        Dictionary<string, MethodInfo> map = null;

        switch (parameters.Count)
        {
            case 0:
                map = NoArgs;
                break;

            case 1:
                map = OneArg;
                break;

            case 2:
                map = TwoArgs;
                checkReverse = true;
                break;

            case 3:
                map = ThreeArgs;
                break;
        }

        if (map.ContainsKey(key))
        {
            return map[key];
        }

        if (checkReverse)
        {
            key = string.Join('.', parameters.Reverse());

            if (map.ContainsKey(key))
            {
                return map[key];
            }
        }

        return null;
    }

    private void GetExpectsParameters(MethodInfo method)
    {
        var parameters = method.GetParameters();

        if (!parameters.Any())
        {
            try
            {
                NoArgs.Add("", method);
            }
            catch
            {
                throw new Exception($"{Name}: verb cannot have more than one Expects method with no parameters");
            }
        }
        else
        {
            var list = new List<string>();

            if (parameters.Length > 3)
            {
                throw new Exception($"{Name}: Expects method can only have 0-3 parameters.");
            }

            void Add(Type t)
            {
                if (t == typeof(Object))
                {
                    list.Add("obj");
                }
                else if (t.BaseType == typeof(Prep))
                {
                    var prep = Activator.CreateInstance(t);

                    if (!AcceptedPrepositions.Contains(prep))
                    {
                        AcceptedPrepositions.Add((Prep)prep);
                    }

                    list.Add($"{prep}");
                }
                else
                {
                    throw new ArgumentException($"{t} is not a valid type for Expects call");
                }

            }

            for (var i = 0; i < parameters.Length; i++)
            {
                if (i == 0)
                {
                    Add(parameters[0].ParameterType);
                }

                if (i == 1)
                {
                    Add(parameters[1].ParameterType);
                }

                if (i == 2)
                {
                    if (parameters[2].ParameterType != typeof(Object))
                    {
                        throw new Exception($"{Name}: Expects method requires Item as third parameter.");
                    }
                    else
                    {
                        list.Add("obj");
                    }
                }

            }

            var key = string.Join('.', list);

            switch (list.Count)
            {
                case 1:
                    OneArg.Add(key, method);
                    break;
                case 2:
                    TwoArgs.Add(key, method);
                    break;
                case 3:
                    ThreeArgs.Add(key, method);
                    break;
                default:
                    throw new Exception($"{Name}: Expects method can only have 0-3 parameters");
            }

        }
    }

    private List<MethodInfo> GetExpectsMethods()
    {
        var verbType = GetType();

        return (
            from m in verbType.GetMethods()
            where m.Name == "Expects"
            select m
        ).ToList();
    }

    public string Name { get; protected set; }

    public Synonyms Synonyms = new();

    /// <summary>
    /// Accepts multiple items (objects in scope)
    /// </summary>
    public bool Multi { get; set; }

    /// <summary>
    /// Accepts multiple held items (direct object must be in inventory)
    /// </summary>
    public bool MultiHeld { get; set; }
    
    public static T Get<T>() where T : Verb
    {
        return (T)Verbs.List.Single(x => x is T);
    }

    public static bool Redirect<T>(Object item, Func<T, bool> callback) where T : Verb
    {
        return Object.Redirect(item, callback);
    }

    public static bool Redirect<T>(Func<T, bool> callback) where T : Verb
    {
        var room = Player.Location;
        return Object.Redirect(room, callback);
    }

    protected static bool Print(string message, CommandState? state = null)
    {
        if (Context.Current == null)
        {
            Output.Print(message);
        }
        else
        {
            Context.Current.Print(message, state);
        }

        return true;
    }
}