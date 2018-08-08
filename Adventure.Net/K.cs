namespace Adventure.Net
{
    internal class K
    {
        public const string ALL = "all";
        public const string EXCEPT = "except";
        public const string OBJECT_TOKEN = "x";
        public const string INDIRECT_OBJECT_TOKEN = "y";
        
        // object that exists in game
        public const string NOUN_TOKEN = "<noun>";
        
        // object must be in inventory
        public const string HELD_TOKEN = "<held>";
        
        // can refer to multiple objects
        public const string MULTI_TOKEN = "<multi>";
        
        // can refer to multiple objects in inventory
        public const string MULTIHELD_TOKEN = "<multiheld>";
        
        // can be any word
        public const string TOPIC_TOKEN = "<topic>";
        
        public const string DIRECTION_TOKEN = "<direction>";

    }
}
