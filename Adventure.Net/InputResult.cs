using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Net.Verbs;

namespace Adventure.Net
{
    public class InputResult
    {
        public List<Object> Objects { get; set; }
        public Object IndirectObject { get; set; }
        public List<Object> Exceptions { get; private set; }
        public string Pregrammar { get; set; }
        public bool IsAll { get; set; }
        public string Preposition { get; set; }
        public bool IsAskingQuestion { get; set; }
        public bool IsPartial { get; set; }
        public bool Handled { get; set; }

        // contains parser results for recursive calls
        public List<string> ParserResults { get; set; }
 
        private Verb _verb;
        private Grammar _grammar;

        public InputResult()
        {
            Objects = new List<Object>();
            Exceptions = new List<Object>();
            Verb = new NullVerb();
            ParserResults = new List<string>();
        }

        public Verb Verb
        {
            get { return _verb; }
            set { _verb = value ?? new NullVerb(); }
        }

        public bool IsSingleAction
        {
            get
            {
                return ((Verb.IsNull && Action != null) ||
                (Verb is DirectionalVerb) ||
                (GetGrammar("") != null));
            }
        }

        public Grammar Grammar
        {
            get { return _grammar; }
            set
            {
                _grammar = value;

                if (_grammar == null || string.IsNullOrEmpty(_grammar.Format))
                    ObjectsMustBeHeld = false;
                else
                {
                    var tags = _grammar.Format.Tags();
                    ObjectsMustBeHeld = tags.Count > 0 && (tags[0] == K.HELD_TOKEN || tags[0] == K.MULTIHELD_TOKEN);    
                }

                if (_grammar != null)
                {
                    Action = _grammar.Action;
                    Preposition = _grammar.Preposition;
                }

                
            }
        }

        public Func<bool> Action { get; set; }
        

        public bool ObjectsMustBeHeld { get; private set; }

        public bool IsExcept { get; set; }

        private Grammar GetGrammar(string format)
        {
            return Verb.Grammars.SingleOrDefault(x => x.Format == format);
        }
    }

}
