using Adventure.Net.Actions;

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
                var result = Item.Execute<Take>(obj, v => v.Expects(obj));

                // only return result.Success

                if (result.Success)
                {
                    Context.Current.Print($"(first taking the {obj.Name})", CommandState.After);
                }

                // need to print messages regardless of success/fail
                foreach (var message in result.CommandOutput.Output)
                {
                    Context.Current.Print(message);
                }

                return result.Success;
            }

            // pass thru - keep on processing
            return true;
        }
    }
}
