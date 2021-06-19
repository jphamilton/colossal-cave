using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    // TODO: Need to implement "(the) {obj}" in output - Look at Article handling in inform and double check the Prints for Verbs there

    public class AfterRoutines
    {
        private readonly Dictionary<Type, Action> routines = new();

        public void Add(Type type, Action after)
        {
            if (routines.ContainsKey(type))
            {
                var routine = routines[type];

                void wrapper()
                {
                    routine();
                    after();
                }

                routines.Remove(type);

                routines.Add(type, wrapper);
            }
            else
            {
                routines.Add(type, after);
            }
            
        }

        public Action Get(Type verbType)
        {
            if (routines.ContainsKey(verbType))
            {
                return routines[verbType];
            }

            return null;
        }
    }
}
