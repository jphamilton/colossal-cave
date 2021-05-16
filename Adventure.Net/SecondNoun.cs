namespace Adventure.Net
{
    public class SecondNoun
    {
        public static bool Is<T>() where T : Item
        {
            Item obj = Items.Get<T>();
            return Context.IndirectObject == obj;
        }
    }
}
