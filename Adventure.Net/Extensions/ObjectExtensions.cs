namespace Adventure.Net.Extensions
{
    public static class ObjectExtensions
    {
        public static bool Is<T>(this Object obj) where T: Object
        {
            return obj != null && obj is T;
        }

        public static bool IsNot<T>(this Object obj) where T : Object
        {
            return obj != null && !(obj is T);
        }
    }
}
