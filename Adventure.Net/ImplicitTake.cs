﻿using Adventure.Net.Verbs;

namespace Adventure.Net
{
    public class ImplicitTake : IInvoke
    {
        private readonly Item obj;

        public ImplicitTake(Item obj)
        {
            this.obj = obj;
        }

        public bool Invoke()
        {
            if (!obj.InInventory)
            {
                var result = obj.Execute<Take>(obj, v => v.Expects(obj));

                // only return result.Success

                if (result.Success)
                {
                    Context.Current.Print($"(first taking the {obj.Name})", CommandState.After);
                }

                foreach (var message in result.CommandOutput.Output)
                {
                    Context.Current.Print(message);
                }

                return result.Success;
            }

            return true;
        }
    }
}
