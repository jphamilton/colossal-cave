using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{
    public abstract class Verb 
    {
        // ()
        private Dictionary<string, MethodInfo> NoArgs = new Dictionary<string, MethodInfo>();
        // (obj)
        private Dictionary<string, MethodInfo> OneArg = new Dictionary<string, MethodInfo>();
        // (obj, prep)
        private Dictionary<string, MethodInfo> TwoArgs = new Dictionary<string, MethodInfo>();
        // (obj, prep, indirect)
        private Dictionary<string, MethodInfo> ThreeArgs = new Dictionary<string, MethodInfo>();

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
                    if (t == typeof(Item))
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
                        if (parameters[2].ParameterType != typeof(Item))
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

            var methods = (
                from m in verbType.GetMethods()
                where m.Name == "Expects"
                select m
            ).ToList();
            return methods;
        }


        public string Name { get; protected set; }

        public Synonyms Synonyms = new Synonyms();

        /// <summary>
        /// Summary accepts multiple items (objects in scope)
        /// </summary>
        public bool Multi { get; set; }

        /// <summary>
        /// Summary accepts multiple items (objects in inventory)
        /// </summary>
        public bool MultiHeld { get; set; }

        public static T Get<T>() where T : Verb
        {
            return (T)VerbList.List.Single(x => x is T);
        }

        public static Verb Get(Type type) 
        {
            return VerbList.List.Single(x => x.GetType() == type);
        }

        public static bool Redirect<T>(Item item, Func<T, bool> callback) where T : Verb
        {
            return item.Redirect(item, callback);
        }

        protected void Print(string message, CommandState? state = null)
        {
            Context.Current.Print(message, state);
        }
    }
}