namespace Adventure.Net
{
    public class Noun
    {
        public static bool Is<T>() where T:Item
        {
            Item obj = Items.Get<T>();
            return Context.Object == obj;
        }
    }
}
