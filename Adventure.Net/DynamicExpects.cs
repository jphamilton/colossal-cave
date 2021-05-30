using Adventure.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{
    public class DynamicExpects : IInvoke
    {
        private readonly Type verbType;
        private readonly DynamicCall call;
        
        public MethodInfo Expects { get; private set; }

        public DynamicExpects(Verb verb, DynamicCall call)
        {
            this.call = call;
            this.call = call;

            verbType = verb.GetType();
            
            Expects = verbType.GetMethod("Expects", call.Types);
        }

        public bool Invoke()
        {
            var invoker = new Invoker();

            if (Expects != null)
            {
                ParameterInfo[] parameters = Expects.GetParameters();

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
                            throw new ArgumentException("[Held] attribute is only for objects");
                        }
                    }
                });


                invoker.Add(new DymamicInvoke(Expects, verbType, call.Args));

                return invoker.Invoke();
            }

            return false;
        }

        //public IList<Prep> AcceptedPrepositions()
        //{
        //    return (
        //            from m in verbType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
        //            from p in m.GetParameters()
        //            where m.Name == "Expects" && p.ParameterType.IsSubclassOf(typeof(Prep))
        //            select p
        //        )
        //        .Select(x => (Prep)Activator.CreateInstance(x.ParameterType))
        //        .ToList();
        //}

    }
}
