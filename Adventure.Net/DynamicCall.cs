using System;
using System.Collections.Generic;
using System.Reflection;

namespace Adventure.Net
{
    public class DynamicCall
    {
        public Type[] Types { get; private set; }
        public object[] Args { get; private set; }

        public DynamicCall(MethodInfo method, Item obj, Prep prep, Item indirect)
        {
            Initialize(method, obj, prep, indirect);
        }
                
        private void Initialize(MethodInfo method, Item obj, Prep prep, Item indirect)
        {
            var parameters = method.GetParameters();

            var types = new List<Type>();
            var args = new List<object>();

            foreach(var parameter in parameters)
            {
                if (parameter.ParameterType == typeof(Item))
                {
                    if (args.Contains(obj))
                    {
                        args.Add(indirect);
                    }
                    else
                    {
                        args.Add(obj);
                    }

                    types.Add(parameter.ParameterType);
                }
                else if (parameter.ParameterType.IsSubclassOf(typeof(Prep)))
                {
                    args.Add(prep);
                    types.Add(parameter.ParameterType);
                }


            }

            Types = types.ToArray();
            Args = args.ToArray();
        }

    }
}
