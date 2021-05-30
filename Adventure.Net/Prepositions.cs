using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public abstract class Prep : IEquatable<Prep>
    {
        public bool Equals(Prep other)
        {
            return GetType() == other.GetType();
        }
    }

    public static class Preposition
    {
        public class About : Prep { }
        public class Against : Prep { }
        public class Apart : Prep { }
        public class At : Prep { }
        public class Down : Prep { }
        public class For : Prep { }
        public class From : Prep { }
        public class In : Prep { }
        public class Inside : Prep { }
        public class Into : Prep { }
        public class Of : Prep { }
        public class Off : Prep { }
        public class On : Prep { }
        public class Onto : Prep { }
        public class Out : Prep { }
        public class Over : Prep { }
        public class Through : Prep { }
        public class To : Prep { }
        public class Top : Prep { }
        public class Under : Prep { }
        public class Up : Prep { }
        public class With : Prep { }
    }
    

    public class Prepositions 
    {

        private static Dictionary<string, Prep> map = new()
        {
            {"about", new Preposition.About()},
            {"against", new Preposition.Against()},
            {"apart", new Preposition.Apart()},
            {"at",  new Preposition.At()},
            {"down", new Preposition.Down()},
            {"for", new Preposition.For()},
            {"from", new Preposition.From()},
            {"in", new Preposition.In()},
            {"inside", new Preposition.Inside()},
            {"into", new Preposition.Into()},
            {"of", new Preposition.Of()},
            {"off", new Preposition.Off()},
            {"on", new Preposition.On()},
            {"onto", new Preposition.Onto()},
            {"out", new Preposition.Out()},
            {"over", new Preposition.Over()},
            {"through", new Preposition.Through()},
            {"to", new Preposition.To()},
            {"top", new Preposition.Top()},
            {"under", new Preposition.Under()},
            {"up", new Preposition.Up()},
            {"with", new Preposition.With()},
        };

        public static void Add(string token, Prep prep)
        {
            if (!map.ContainsKey(token))
            {
                map.Add(token, prep);
            }
        }

        public static Prep Get(string token)
        {
            if (map.ContainsKey(token))
            {
                return map[token];
            }

            return null;
        }

        public static bool Contains(string token)
        {
            return map.ContainsKey(token);
        }
        
    }
}
