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
            
            if (Item.IsScenery && string.IsNullOrEmpty(Item.Description))
            {
                Print("You see nothing special about the {0}.", Item.Name);
            }
            else if (Item is Room)
            {
                Print("That's not something you need to refer to in the course of this game.");
            }
            else if (Item.Describe != null)
            {
                string result = Item.Describe();
                if (!String.IsNullOrEmpty(result))
                    Print(result);
                else
                    Print(Item.Description);
                
            }
            else if (!string.IsNullOrEmpty(Item.Description))
            {
                Print(Item.Description);
            }
            else
            {
                Print("That's not something you need to refer to in the course of this game.");
            }

            return true;
        }


    }
}
