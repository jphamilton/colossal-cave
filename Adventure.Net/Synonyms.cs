using System.Collections.Generic;

namespace Adventure.Net
{
    public class Synonyms : List<string>
    {
        public void Are(params string[] values)
        {
            foreach (string value in values)
                Add(value);
        }

        public void Are(string commaSeparatedList)
        {
            string[] values = commaSeparatedList.Split(',');
            foreach (string value in values)
                Add(value.Trim());
        }
    }
}