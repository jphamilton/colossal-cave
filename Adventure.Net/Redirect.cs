using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using System;

namespace Adventure.Net;

// Redirect from one Routine to another
public static class Redirect
{
    public static bool To<T>(Object obj) where T : Routine
    {
        return To<T>(obj, null, v => v.Handler(obj, null));
    }

    public static bool To<T>(Object obj, Object indirect) where T : Routine
    {
        return To<T>(obj, indirect, v => v.Handler(obj, indirect));
    }

    private static bool To<T>(Object obj, Object indirect, Func<T, bool> handler) where T : Routine
    {
        var state = Context.Current?.PushState() ?? new CommandOutput();

        var handled = Run(state, obj, indirect, handler);

        Context.Current?.PopState();
        
        return handled;
    }

    private static bool Run<T>(ICommandState command, Object obj, Object indirectObject, Func<T, bool> handler) where T : Routine
    {
        var handled = false;
        var success = false;
        var type = typeof(T);

        var before = obj.GetBeforeRoutine(type);

        if (before != null)
        {
            command.State = CommandState.Before;
            handled = before();
        }

        if (indirectObject != null)
        {
            before = indirectObject.GetBeforeRoutine(type);

            if (before != null)
            {
                command.State = CommandState.Before;
                handled = before();
            }
        }

        if (!handled)
        {
            // room before, Action Routine, room afer
            success = HandleAction(command, obj, indirectObject, handler);

            if (success)
            {
                var after = obj.GetAfterRoutine(type);

                if (after != null)
                {
                    command.State = CommandState.After;
                    after();
                }

                if (indirectObject != null)
                {
                    after = indirectObject.GetAfterRoutine(type);

                    if (after != null)
                    {
                        command.State = CommandState.After;
                        after();
                    }
                }
            }
        }

        return success;
    }

    private static bool HandleAction<T>(ICommandState command, Object obj, Object indirectObject, Func<T, bool> handler) where T: Routine
    {
        var handled = false;
        var success = false;
        var type = typeof(T);

        // room before
        var before = Player.Location.GetBeforeRoutine(type);

        if (before != null)
        {
            command.State = CommandState.Before;
            handled = before();
        }

        if (!handled)
        {
            command.State = CommandState.During;

            // The Action Routine (Take, Put, Enter, etc.)
            success = handler(Routines.Get<T>());

            if (success)
            {
                // room after
                var after = Player.Location.GetAfterRoutine(type);

                if (after != null)
                {
                    command.State = CommandState.After;
                    after();
                }
            }
        }

        return success;
    }
}
