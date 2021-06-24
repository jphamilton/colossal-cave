using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Net.Actions
{
    /// <summary>
    /// Helpful for testing
    /// </summary>
    public class Purloin : Verb
    {
        public Purloin()
        {
            Name = "purloin";
            InScopeOnly = false;
        }

        public bool Expects(Object obj)
        {
            obj.Remove();
            Inventory.Add(obj);
            Print("[Purloined.]");
            return true;
        }
    }
}
