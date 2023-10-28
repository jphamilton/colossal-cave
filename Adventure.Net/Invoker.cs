using System.Collections.Generic;

namespace Adventure.Net;

internal class Invoker : IInvoke
{
    private List<IInvoke> routines;

    public Invoker()
    {
        routines = new List<IInvoke>();
    }

    public void Add(IInvoke routine)
    {
        routines.Add(routine);
    }

    public bool Invoke()
    {
        bool success = true;

        foreach (var routine in routines)
        {
            if (!routine.Invoke())
            {
                success = false;
                break;
            }
        }

        return success;
    }
}
