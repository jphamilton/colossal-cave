using Adventure.Net.ActionRoutines;
using Adventure.Net.Extensions;
using Adventure.Net.Things;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;

public static class ImplicitTaker
{
    public static bool CanTake(Parsed pr)
    {
        // we need 1 object, 1 routine and that routine
        // cannot be Drop (because taking something and dropping it right back would be dumb)
        if (pr.Objects.Count != 1 || pr.PossibleRoutines.Count != 1 || pr.Routine is Drop)
        {
            return false;
        }

        var routine = pr.PossibleRoutines[0];

        var obj = pr.Objects.First();

        return
            CanTake(obj) &&
            routine != Routines.Get<Take>() && !routine.GetType().IsSubclassOf(typeof(Take)) &&
            routine.Requires.Count > 0;
    }

    public static bool CanTake(Object obj)
    {
        return
            !obj.Animate &&
            !obj.Scenery && !obj.Static && !obj.Absent &&
            obj.Parent is not Player &&
            obj is not Room;
    }

    public static bool TryGetImplicitObject(Routine routine, string prep, O flag, out Object obj, out string aside)
    {
        obj = routine.ImplicitObject?.Invoke(flag);
        aside = null;

        if (obj != null)
        {
            // we only show the prep in the aside if it was not parsed from the input
            prep = !prep.HasValue() ? routine.Prepositions.FirstOrDefault() : null;

            aside = prep.HasValue() ? $"({prep} {obj.DName})" : $"({obj.DName})";
        }

        return obj != null;
    }

    public static bool Take(Parsed pr, Object obj)
    {
        var taken = Take(pr, [obj]);
        return taken.Count == 1;
    }

    public static List<Object> Take(Parsed pr, List<Object> objects)
    {
        objects = [.. objects.Where(CanTake)];

        if (objects.Count == 0)
        {
            return [];
        }

        var taken = new List<Object>();

        var parserResult = new ParserResult
        {
            Objects = objects,
            IndirectObject = null,
            Routine = Routines.Get<ImplicitTake>()
        };

        Context.Current = new CommandContext(parserResult);
        Context.Current.PushState();

        foreach (var obj in objects)
        {
            if (Implicit.Action<Take>(obj))
            {
                taken.Add(obj);
            }
            else
            {
                pr.Objects = [.. pr.Objects.Where(x => x != obj)];
            }

            if (Context.Story.IsDone)
            {
                break;
            }
        }

        Context.Current.PopState();

        List<string> output = [.. Context.Current.Messages];

        Context.Current = null;

        Output.Print(output);

        pr.IsHandled = Context.Story.IsDone || taken.Count == 0;

        return taken;
    }
}
