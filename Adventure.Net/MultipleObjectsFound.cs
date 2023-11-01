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
    }
}
