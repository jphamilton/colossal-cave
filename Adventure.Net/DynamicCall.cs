using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net
{
    public class DynamicCall
    {
        public Type[] Types { get; private set; }
        public object[] Args { get; private set; }

        public DynamicCall(Item obj, Prep prep, Item indirect)
        {
            Initialize(obj, prep, indirect);
        }

        public DynamicCall(Parameters result)
        {
            Initialize(result.Objects.FirstOrDefault(), result.Preposition, result.IndirectObject);
        }

        private void Initialize(Item obj, Prep prep, Item indirect)
        {
            var types = new List<Type>();
            var args = new List<object>();

            if (obj != null)
            {
                types.Add(typeof(Item));
                args.Add(obj);
            }

            if (prep != null)
            {
                types.Add(prep.GetType());
                args.Add(prep);
            }

            if (indirect != null)
            {
                types.Add(typeof(Item));
                args.Add(indirect);
            }

            Types = types.ToArray();
            Args = args.ToArray();
        }

    }
}
