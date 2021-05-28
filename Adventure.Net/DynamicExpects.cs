using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{
    public partial class CommandHandler
    {
        private class DynamicExpects
        {
            private readonly Type verbType;
            private readonly DynamicCall call;

            public DynamicExpects(Type verbType, DynamicCall call)
            {
                this.verbType = verbType;
                this.call = call;
            }

            public bool Invoke()
            {
                var expects = verbType.GetMethod("Expects", call.Types);

                var instance = Activator.CreateInstance(verbType);

                if (expects != null)
                {
                    return (bool)expects.Invoke(instance, call.Args);
                }

                var verb = verbType.ToString().Split('.').Last();
                var args = new List<string>();
                
                foreach(var type in call.Types)
                {
                    args.Add(type.ToString().Split('.').Last());
                }

                throw new MissingMethodException($"{verb}.Expects({string.Join(",", args)}) is missing");
            }
        }
    }
}
