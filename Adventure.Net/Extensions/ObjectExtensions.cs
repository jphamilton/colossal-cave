namespace Adventure.Net.Extensions
{
    public static class ObjectExtensions
    {
        public static bool Is<T>(this Item obj) where T: Item
        {
            return obj != null && obj is T;
        }

        public static bool IsNot<T>(this Item obj) where T : Item
        {
            return obj != null && !(obj is T);
        }
    }
}
