﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{
    public static class VerbList 
    {
        private static List<Verb> verbs;

        public static void Load()
        {
            verbs = new List<Verb>();

            Action<IEnumerable<Type>> add = (list) =>
            {
                foreach (var type in list)
                {
                    if (type.IsSubclassOf(typeof(Verb)) && !type.IsAbstract)
                    {
                        var instance = Activator.CreateInstance(type) as Verb;
                        
                        instance.Initialize();
                        
                        verbs.Add(instance);
                    }
                }
                
            };

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            add(types);

            var storyType = Context.Story.GetType();
            types = Assembly.GetAssembly(storyType).GetTypes();
            add(types);
            
        }

        public static IList<Verb> List
        {
            get { return verbs; }
        }

        public static Verb GetVerbByName(string name)
        {
            return List.Where(x => x.Name == name || x.Synonyms.Contains(name)).FirstOrDefault();
        }

        public static IList<Verb> GetVerbsByName(string name)
        {
            return List.Where(x => x.Name == name || x.Synonyms.Contains(name)).ToList();
        }

    }
}