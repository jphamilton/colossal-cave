using Adventure.Net.Actions;
using System.Linq;
using System.Transactions;

namespace Adventure.Net;

public class ImplicitTake : IInvoke
{
    private readonly Object obj;

    public ImplicitTake(Object obj)
    {
        this.obj = obj;
    }

    public bool Invoke()
    {
        if (!obj.InInventory)
        {
            var result = Object.Execute<Take>(obj, v => v.Expects(obj));

            // only return result.Success

            if (result.Success)
            {
                Context.Current.Print($"(first taking the {obj.Name})", CommandState.After);
            }

            // filter out "Taken."
            var messages = result.CommandOutput.Messages.Where(x => !x.Contains("Taken."));

            // before/after messages need to be displayed
            foreach (var message in messages)
            {
                Context.Current.Print(message);
            }

            return result.Success;
        }

        // pass thru - keep on processing
        return true;
    }
}
