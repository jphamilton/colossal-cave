namespace Adventure.Net
{
    public class ContextObject
    {
        protected Item Item
        {
            get { return Context.Item; }
            set { Context.Item = value; }
        }

        protected Item IndirectItem
        {
            get { return Context.IndirectItem; }
            set { Context.IndirectItem = value; }
        }

        public void Print(string format, params object[] arg)
        {
            if (!SupressMessages)
                Context.Parser.Print(format, arg);
        }

        public void Print(string text)
        {
            if (!SupressMessages)
                Context.Parser.Print(text);
        }

        public bool SupressMessages { get; set; }
    }
}
