using System;

namespace Adventure.Net.Verbs
{
    public class Examine : Verb
    {
        public Examine()
        {
            Name = "examine";
            Synonyms.Are("examine", "x", "check", "describe", "watch");
            Grammars.Add("<noun>", ExamineObject);
        }

        public bool ExamineObject()
        {
            Library L = new Library();

            if (!L.IsLit())
            {
                Print("Darkness, noun. An absence of light to see by.");
                return true;
            }
            
            if (Object.IsScenery && string.IsNullOrEmpty(Object.Description))
            {
                Print("You see nothing special about the {0}.", Object.Name);
            }
            else if (Object is Room)
            {
                Print("That's not something you need to refer to in the course of this game.");
            }
            else if (Object.Describe != null)
            {
                string result = Object.Describe();
                if (!String.IsNullOrEmpty(result))
                    Print(result);
                else
                    Print(Object.Description);
                
            }
            else if (!string.IsNullOrEmpty(Object.Description))
            {
                Print(Object.Description);
            }
            else
            {
                Print("That's not something you need to refer to in the course of this game.");
            }

            return true;
        }


    }
}
