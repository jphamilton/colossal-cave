namespace Adventure.Net.Actions;

//Verb 'pick'
//    * 'up' multi                                -> Take
//    * multi 'up'                                -> Take;
public class Pick : Verb
{
    public Pick()
    {
        Name = "pick up";
        Synonyms.Are("pick");
        Multi = true;
    }

    public bool Expects(Object obj, Preposition.Up up)
    {
        return Redirect<Take>(obj, v => v.Expects(obj));
    }

}
