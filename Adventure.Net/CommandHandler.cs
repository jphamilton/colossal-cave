using Adventure.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net
{
    public partial class CommandHandler
    {
        private readonly CommandLineParserResult parsed;
        private readonly CommandResult result;
        private readonly List<Item> objects;
        private readonly Item indirectObject;
        private readonly Verb verb;
        private readonly Type verbType;
        private readonly Prep preposition;

        public CommandHandler(CommandLineParserResult parsed)
        {
            this.parsed = parsed;

            verb = parsed.Verb;
            objects = parsed.Objects;
            preposition = parsed.Preposition;
            indirectObject = parsed.IndirectObject;
            verbType = verb?.GetType();

            result = new CommandResult
            {
                Success = false,
            };
        }

        public CommandResult Run()
        {
            if (parsed.Error.HasValue())
            {
                HandleError(parsed.Error);
                return result;
            }

            var context = new CommandContext(parsed);
            var commandState = context.PushState();
            var failed = false;

            Context.Current = context;

            if (objects.Count > 0)
            {
                failed = HandleObjects(context, commandState);
            }
            else
            {
                // one word command (e.g. look, i, south)
                Expects(verb, null);
            }

            context.PopState();

            result.Success = !failed;

            Print(context.Output);

            Context.Current = null;

            return result;
        }

        private bool HandleObjects(CommandContext context, ICommandState commandState)
        {
            bool failed = false;

            foreach (var obj in objects)
            {
                context.CurrentObject = obj;

                bool handled = false;

                var before = obj.Before(verbType);

                if (before != null)
                {
                    commandState.State = CommandState.Before;
                    handled = before();
                }

                if (indirectObject != null)
                {
                    before = indirectObject.Before(verbType);

                    if (before != null)
                    {
                        commandState.State = CommandState.Before;
                        handled = before();
                    }
                }

                if (!handled)
                {
                    commandState.State = CommandState.During;
                    bool success = Expects(verb, obj);

                    if (success)
                    {
                        var after = obj.After(verbType);

                        if (after != null)
                        {
                            commandState.State = CommandState.After;
                            after();
                        }

                        if (indirectObject != null)
                        {
                            after = indirectObject.After(verbType);

                            if (after != null)
                            {
                                commandState.State = CommandState.After;
                                after();
                            }
                        }
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

        private void HandleError(string error)
        {
            Context.Current = null;

            result.Error = error;
            result.Success = false;

            Print(error);
        }

        private bool Expects(Verb verb, Item obj)
        {
            var call = new DynamicCall(parsed.Expects, obj, preposition, indirectObject);
            var expects = new DynamicExpects(verb, parsed.Expects, call);

            var handled = false;
            var success = false;

            var before = CurrentRoom.Location.Before(verb.GetType());

            if (before != null)
            {
                handled = before();
            }

            if (!handled)
            {
                success = expects.Invoke();

                if (success)
                {
                    var after = CurrentRoom.Location.After(verb.GetType());

                    if (after != null)
                    {
                        after();
                    }
                }
            }
            
            
            return success;
        }

        private void Print(string message)
        {
            Output.Print(message);
            result.Output.Add(message);

        }

        private void Print(IEnumerable<string> messages)
        {
            if (messages.Any())
            {
                Output.Print(messages);
                result.Output.AddRange(messages);
            }
        }
    }
}
