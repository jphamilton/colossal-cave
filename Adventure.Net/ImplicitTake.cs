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
        // object is not in inventory or object is in inventory but is in a container
        if (!obj.InInventory)
        {
            var parent = obj.Parent;

            var result = Object.Execute<Take>(obj, v => v.Expects(obj));

            if (result.Success)
            {
                string loc = "";

                if (parent != null)
                {
                    if (parent is Container container)
                    {
                        loc = $" out of {container.DefiniteArticle} {container.Name}";
                    }
                    else if (parent is Supporter supporter)
                    {
                        loc = $" off of {supporter.DefiniteArticle} {supporter.Name}";
                    }
                }

                Context.Current.Print($"(first taking the {obj.Name}{loc})", CommandState.After);
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
