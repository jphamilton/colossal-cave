namespace ColossalCave.Actions;

/*
 Verb 'pour' 'douse'
    * 'water' 'on' noun     -> Water
    * 'oil' 'on' noun       -> Oil
    * noun                  -> Empty;
 */
public class PourWater : Water
{
    public PourWater()
    {
        Verbs = ["pour water"];
        Prepositions = ["on"];
        ImplicitObject = (_) => ObjectsInScope.Find(x => x.Before<Water>());
    }
}

public class PourOil : Oil
{
    public PourOil()
    {
        Verbs = ["pour oil"];
        Prepositions = ["on"];
        ImplicitObject = (_) => ObjectsInScope.Find(x => x.Before<Oil>());
    }
}