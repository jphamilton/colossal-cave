using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;

public partial class Parser
{
    // Which do you mean, the red hat, the black hat or the white hat?
    private class MultipleObjectsFound : Object
    {
        public IList<Object> Objects { get; }

        public MultipleObjectsFound(IList<Object> objects)
        {
            Objects = objects;
        }

        public override void Initialize()
        {
            // no op
        }

        public string List()
        {
            var x = Objects.Select(x => $"{x.DefiniteArticle} {x.Name}").ToList();

            if (x.Count < 3)
            {
                return string.Join(" or ", x);
            }

            return string.Join(", ", x.GetRange(0, x.Count - 1)) + $" or {x.Last()}";
        }
    }
}
