using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using Handler = System.Func<Adventure.Net.Object, Adventure.Net.Object, bool>;

namespace Adventure.Net;

public class Command
{
    private readonly Type type;
    private readonly Routine routine;
    private readonly List<Object> objects;
    private readonly Object ind;
    private readonly TokenizedInput input;
    private readonly Handler handler;
    private readonly ICommandState command;

    public Command(ParserResult pr)
    {
        routine = pr.Routine;
        type = routine.GetType();
        handler = routine.Handler;
        objects = pr.Objects;
        ind = pr.IndirectObject;
        input = pr.Parsed.Input;

        Context.Current = new CommandContext(pr);
        command = Context.Current.PushState();
    }

    public CommandResult Run()
    {
        var result = new CommandResult
        {
            Success = false,
        };

        var failed = false;
        var died = false;
        
        try
        {
            if (routine is ForwardTokens forward)
            {
                failed = !forward.Handle(input);
            }
            else if (objects.Count > 0)
            {
                failed = HandleObjects();
            }
            else
            {
                // one word command (e.g. look, i, south)
                HandleAction(null);
            }

            result.Success = !failed;

            if (Context.Current?.Move != null)
            {
                Player.Location = Context.Current.Move();
            }
        }
        catch (DeathException)
        {
            // player died somehow :(
            died = true;
        }

        if (Context.Current.PopState())
        {
            result.Output = [.. Context.Current.Messages];
        }

        Context.Current = null;

        if (died)
        {
            Output.Print(result.Output);
            result.Output.Clear();

            var dead = Routines.GetDeathRoutine() ?? throw new NotImplementedException("Dead routine has not been implemented");
            result.Success = dead.Handler(null, null);
        }

        return result;
    }

    private bool HandleObjects()
    {
        bool failed = false;
        bool indCalled = false;

        foreach (var obj in objects)
        {
            // Context.Current.Second is set to the indirect object in the CommandContext ctor
            Context.Current.First = obj;

            var handled = HandleBeforeObject(obj);

            if (ind != null && !indCalled)
            {
                var before = ind.GetBeforeRoutine(type);

                if (before != null)
                {
                    command.State = CommandState.Before;
                    handled = before();
                }

                indCalled = true;
            }

            if (!handled)
            {
                command.State = CommandState.During;
                
                // before room, action routine, after room
                bool success = HandleAction(obj);

                if (success)
                {
                    HandleAfterObject(command, obj);
                }
                else
                {
                    failed = true;
                    break;
                }
            }
        }

        return failed;
    }

    private bool HandleBeforeIndirect()
    {
        bool handled = false;

        // before indirect
        if (ind != null)
        {
            var before = ind.GetBeforeRoutine(type);

            if (before != null)
            {
                command.State = CommandState.Before;
                handled = before();
            }
        }

        return handled;
    }

    private bool HandleBeforeObject(Object obj)
    {
        bool handled = false;

        // before object
        var before = obj.GetBeforeRoutine(type);

        if (before != null)
        {
            command.State = CommandState.Before;
            handled = before();
        }

        if (!handled)
        {
            handled = HandleBeforeIndirect();
        }

        return handled;
    }

    private bool HandleAction(Object obj)
    {
        var handled = false;
        var success = false;

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
            success = handler(obj, ind);

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

    private void HandleAfterObject(ICommandState command, Object obj)
    {
        // after object
        var after = obj.GetAfterRoutine(type);

        if (after != null)
        {
            command.State = CommandState.After;
            after();
        }

        HandleAfterIndirect();
    }

    private void HandleAfterIndirect()
    {
        if (ind != null)
        {
            var after = ind.GetAfterRoutine(type);

            if (after != null)
            {
                command.State = CommandState.After;
                after();
            }
        }
    }
}
