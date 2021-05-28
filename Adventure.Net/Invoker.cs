using System.Collections.Generic;

namespace Adventure.Net
{
    internal class Invoker : IInvoke
    {
        private List<IInvoke> routines;

        public Invoker()
        {
            routines = new List<IInvoke>();
        }

        public void Add(IInvoke routine)
        {
            routines.Add(routine);
        }

        public bool Invoke()
        {
            foreach(var routine in routines)
            {
                if (!routine.Invoke())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
