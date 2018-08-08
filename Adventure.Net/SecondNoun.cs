namespace Adventure.Net
{
    public class SecondNoun
    {
        public static bool Is<T>() where T : Object
        {
            Object obj = Objects.Get<T>();
            return Context.IndirectObject == obj;
        }
    }
}
