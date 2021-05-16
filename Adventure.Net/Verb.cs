using System.Linq;
using Adventure.Net.Verbs;

namespace Adventure.Net
{
    public abstract class Verb : ContextObject
    {
        public string Name { get; protected set; }
        
        public Synonyms Synonyms = new Synonyms();
        
        public Grammars Grammars = new Grammars();

        public bool ImplicitTake { get; set; }


        public bool AllowsMulti
        {
            get
            {
                return Grammars.Any(x => x.Format.Contains("<multi"));
            }
        }

        protected bool RedirectTo<T>(string format) where T : Verb, new()
        {
            bool result = false;
            var verb = new T();
            var g = verb.Grammars.SingleOrDefault(x => x.Format == format);

            if (g != null)
            {
                var command =
                    new Command
                        {
                            Object = Context.Item,
                            IndirectObject = Context.IndirectItem,
                            Verb = verb,
                            Action = g.Action
                        };

                result = Context.Parser.ExecuteCommand(command);
            }

            return result;
        }

        public bool IsNull
        {
            get { return this.GetType() == typeof (NullVerb); }
        }
    }
}