using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public partial class CommandHandler
    {
        private class DynamicCall
        {
            public Type[] Types { get; }
            public object[] Args { get; }

            public DynamicCall(Item obj, Preposition? prep, Item indirect)
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
                    types.Add(typeof(Preposition));
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
}
