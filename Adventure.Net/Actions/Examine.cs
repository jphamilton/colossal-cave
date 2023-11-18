using Adventure.Net.Extensions;

namespace Adventure.Net.Actions;

public class Examine : Verb
{
    public Examine()
    {
        Name = "examine";
        Synonyms.Are("examine", "x", "check", "describe", "watch");
    }

    public bool Expects(Object obj)
    {
        if (!CurrentRoom.IsLit())
        {
            return Print("Darkness, noun. An absence of light to see by.");
        }

        if (obj.Scenery && string.IsNullOrEmpty(obj.Description))
        {
            return Print($"You see nothing special about the {obj.Name}.");
        }
        else if (obj is Room)
        {
            return Print("That's not something you need to refer to in the course of this game.");
        }
        else if (obj.Describe != null)
        {
            string result = obj.Describe();
            if (result.HasValue())
            {
                return Print(result);
            }
            else
            {
                return Print(obj.Description);
            }
        }
        else if (obj.Description.HasValue())
        {
            return Print(obj.Description);
        }
        else
        {
            return Print("That's not something you need to refer to in the course of this game.");
        }
    }


}
