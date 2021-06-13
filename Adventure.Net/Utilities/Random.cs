using System;

namespace Adventure.Net.Utilities
{
    public static class Random
    {
        private static System.Random random;

        static Random()
        {
            random = new System.Random(Guid.NewGuid().GetHashCode());
        }
            
        public static int Number(int min, int max)
        {
            return random.Next(min, max);
        }

    }
}
