using Adventure.Net.Extensions;

namespace Adventure.Net.Actions;


public class Open : Verb
{
    public Open()
    {
        Name = "open";
        Synonyms.Are("open, uncover, undo, unwrap");
    }

    public bool Expects(Object obj)
    {
        if (!obj.Openable)
        {
            return Print($"{obj.TheyreOrThats} not something you can open.");
        }
        else if (obj.Locked)
        {
            string seems = obj.PluralName ? "They seem" : "It seems";
            return Print($"{seems} to be locked.");
        }
        else if (obj.Open)
        {
            return Print($"{obj.TheyreOrThats} already open.");
        }
        else
        {
            obj.Open = true;

            if (obj.Transparent)
            {
                return Print($"You open {obj.DefiniteArticle} {obj.Name}.");
            }
            else
            {
                if (obj is Container container && container.Children.Count > 0)
                {
                    return Print($"You open {obj.DefiniteArticle} {obj.Name}, revealing {obj.Children.DisplayList(false)}.");
                }

                return Print($"You open {obj.DefiniteArticle} {obj.Name}.");
            }
        }
    }

    public bool Expects(Object obj, Preposition.With with, Object indirect)
    {
        return Redirect<Unlock>(obj, v => v.Expects(obj, with, indirect));
    }

}
