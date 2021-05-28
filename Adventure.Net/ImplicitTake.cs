using Adventure.Net.Verbs;

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
                // Context.Current.Print($"(first taking the {obj.Name})");

                var result = obj.Execute<Take>(obj, v => v.Expects(obj));

                if (result.Success)
                {
                    Context.Current.Print($"(first taking the {obj.Name})", CommandState.After);
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}
