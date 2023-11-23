using Adventure.Net.Actions;
using Adventure.Net.Things;
using System.Linq;

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
        if (obj.Parent is not InventoryRoot && !obj.Animate)
        {
            var parent = obj.Parent;

            var result = Object.Execute<Take>(obj, v => v.Expects(obj));

            if (result.Success)
            {
                string aside = "";

                if (parent != null)
                {
                    if (parent is Container container)
                    {
                        aside = $"out of {container.DefiniteArticle} {container.Name}";
                    }
                    else if (parent is Supporter supporter)
                    {
                        aside = $"off of {supporter.DefiniteArticle} {supporter.Name}";
                    }
                }

                var message = $"first taking the {obj.Name} {aside}".Trim();
                var output = $"({message})";

                Context.Current.Print(output, CommandState.Before);
            }

            // filter out "Taken."
            var messages = result.CommandOutput.Messages.Where(x => !x.Contains("Taken.") && !x.Contains("You already have that."));

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
