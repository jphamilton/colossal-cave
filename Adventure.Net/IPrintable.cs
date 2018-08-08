namespace Adventure.Net
{
    public interface IPrintable
    {
        void Print(string msg);
        void Print(string format, params object[] arg);
    }
}
