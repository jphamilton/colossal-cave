﻿namespace Adventure.Net
{
    public class ContextObject
    {
        protected Item Object
        {
            get { return Context.Object; }
            set { Context.Object = value; }
        }

        protected Item IndirectObject
        {
            get { return Context.IndirectObject; }
            set { Context.IndirectObject = value; }
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
