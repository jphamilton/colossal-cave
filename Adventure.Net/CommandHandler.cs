using Adventure.Net.Extensions;
using System.Linq;

namespace Adventure.Net
{
    public partial class CommandHandler
    {
        private CommandLineParserResult parsed;

        public CommandHandler(CommandLineParserResult parsed)
        {
            this.parsed = parsed;
        }

        public CommandResult Run()
        {
            if (parsed.Error.HasValue())
            {
                Output.Print(parsed.Error);
                return Error(parsed.Error);
            }

            var context = new CommandContext(parsed);
            var commandState = context.PushState();
            var failed = false;

            Context.Stack.Push(context);

            var verb = parsed.Verb;
            var verbType = verb.GetType();
            
            if (parsed.Objects.Count > 0)
            {
                foreach (var obj in parsed.Objects)
                {
                    context.CurrentObject = obj;

                    bool handled = false;

                    var before = obj.Before(verbType);

                    if (before != null)
                    {
                        commandState.State = CommandState.Before;
                        handled = before();
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
                        }
                        else
                        {
                            failed = true;
                            break;
                        }
                    }

                }

            }
            else
            {
                // one word command (e.g. look, inv)
                Expects(verb, null);
            }

            var result = new CommandResult
            {
                Success = !failed,
            };

            context = Context.Stack.Pop();
            context.PopState();

            // TODO: better way to handle this?
            
            if (failed && !context.Output.Any())
            {
                if (parsed.Ordered.Any() && parsed.Ordered.First() is Item)
                {
                    var obj = (Item)parsed.Ordered.First();

                    //    // e.g. take bottle nonsense nonsense
                    var partialUnderstanding = Messages.PartialUnderstanding(parsed.Verb, obj);
                    result.Output.Add(partialUnderstanding);
                    Output.Print(partialUnderstanding);
                }
                else
                {
                    result.Output.Add(Messages.CantSeeObject);
                    Output.Print(Messages.CantSeeObject);
                }
            }
            else
            {
                var output = context.Output;

                result.Output.AddRange(output);

                Output.Print(output);
            }

            return result;
        }

        private CommandResult Error(string error)
        {
            var result = new CommandResult
            {
                Error = error,
                Success = false
            };

            result.Output.Add(error);

            return result;
        }

        private bool Expects(Verb verb, Item obj)
        {
            var call = new DynamicCall(obj, parsed.Preposition, parsed.IndirectObject);
            var expects = new DynamicExpects(verb, call);

            var success = expects.Invoke();
            
            return success;
        }
    }
}
