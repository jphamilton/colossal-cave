using Adventure.Net.ActionRoutines;
using Adventure.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net;

public static class Routines
{
    private static List<Routine> _routines;

    public static void Load()
    {
        _routines = [];

        static void add(IEnumerable<Type> list)
        {
            var routineTypes = list.Where(t => t.IsSubclassOf(typeof(Routine)) && !t.IsAbstract).ToList();

            foreach (var type in routineTypes)
            {
                var routine = Activator.CreateInstance(type) as Routine;

                _routines.Add(routine);

                Dictionary.AddRoutine(routine);
            }
        }

        // add library routines
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        add(types);

        // add custom routines from game
        var storyType = Context.Story.GetType();
        types = Assembly.GetAssembly(storyType).GetTypes();
        add(types);

    }

    public static List<Routine> List
    {
        get { return _routines; }
    }

    public static List<Routine> Find(string verb, string prep, int requires)
    {
        List<Routine> verbs = [.. _routines.Where(x => x.Verbs.Contains(verb))];
        List<Routine> preps = prep.HasValue() ? [.. verbs.Where(x => x.Verbs.Contains(prep) || x.Prepositions.Contains(prep))] : null;
        
        var found = preps ?? (
            verbs.Count(x => x.Prepositions.Count == 0) > 0
            ? [.. verbs.Where(x => x.Prepositions.Count == 0)]
            : verbs
        );

        if (found.Count > 1)
        {
            if (requires > 0)
            {
                var withRequires = found.Where(x => x.Requires.Count == requires).ToList();
                if (withRequires.Count > 0)
                {
                    found = withRequires;
                }
            }
            else
            {
                var noRequires = found.Where(x => x.Requires.Count == 0).ToList();
                if (noRequires.Count > 0)
                {
                    found = noRequires;
                }

                if (found.Count > 1)
                {
                    // order by placement of verbToken in the Verbs list
                    // e.g. "leave" = Leave will be favored over Out
                    found = [.. found.OrderBy(x => x.Verbs.IndexOf(verb))];
                }
            }
        }

        return found;
    }

    public static T Get<T>() where T : Routine
    {
        return (T)List.FirstOrDefault(x => x.GetType() == typeof(T));
    }

    public static Dead GetDeathRoutine()
    {
        return (Dead)_routines.SingleOrDefault(x => x.GetType().IsSubclassOf(typeof(Dead)));
    }
}