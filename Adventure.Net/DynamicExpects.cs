using Adventure.Net.Extensions;
using System;
using System.Reflection;

namespace Adventure.Net
{

    public partial class CommandHandler
    {
        private class DynamicExpects : IInvoke
        {
            private readonly Type verbType;
            private readonly Verb verb;
            private readonly DynamicCall call;

            public DynamicExpects(Verb verb, DynamicCall call)
            {
                this.verb = verb;
                this.call = call;
                
                verbType = verb.GetType();
            }

            public bool Invoke()
            {
                var invoker = new Invoker();

                var expects = verbType.GetMethod("Expects", call.Types);


                if (expects != null)
                {
                    ParameterInfo[] parameters = expects.GetParameters();

                    parameters.ForEach((parameter, index) =>
                    {
                        var held = parameter.GetCustomAttribute<HeldAttribute>();
                        
                        if (held != null)
                        {
                            var arg = call.Args[index];

                            if (arg is Item)
                            {
                                var implicitTake = new ImplicitTake((Item)arg);
                                invoker.Add(implicitTake);
                            }
                            else
                            {
                                throw new ArgumentException("[Held] attribute is only for Item");
                            }
                        }
                    });
                    

                    invoker.Add(new DymamicInvoke(expects, verbType, call.Args));


                    return invoker.Invoke();
                }

                //var verb = verbType.ToString().Split('.').Last();
                //var args = new List<string>();

                //foreach(var type in call.Types)
                //{
                //    args.Add(type.ToString().Split('.').Last());
                //}

                //throw new MissingMethodException($"{verb}.Expects({string.Join(",", args)}) is missing");
                return false;
            }
        }
    }
}
