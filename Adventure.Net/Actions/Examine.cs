using Adventure.Net.Extensions;
using System;

namespace Adventure.Net.Actions
{
    public class Examine : Verb
    {
        // TODO: implement
        public Examine()
        {
            Name = "examine";
            Synonyms.Are("examine", "x", "check", "describe", "watch");
        }

        // TODO: move all of this to base class Before<Examine> routines
        public bool Expects(Item obj)
        {
            if (!CurrentRoom.IsLit())
            {
                Print("Darkness, noun. An absence of light to see by.");
                return true;
            }

            if (obj.Scenery && string.IsNullOrEmpty(obj.Description))
            {
                Print($"You see nothing special about the {obj.Name}.");
            }
            else if (obj is Room)
            {
                Print("That's not something you need to refer to in the course of this game.");
            }
            else if (obj.Describe != null)
            {
                string result = obj.Describe();
                if (result.HasValue())
                {
                    Print(result);
                }
                else
                {
                    Print(obj.Description);
                }
            }
            else if (obj.Description.HasValue())
            {
                Print(obj.Description);
            }
            else
            {
                Print("That's not something you need to refer to in the course of this game.");
            }

            return true;
        }


    }
}
