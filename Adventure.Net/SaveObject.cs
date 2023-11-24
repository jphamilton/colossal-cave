using System.Collections.Generic;

namespace Adventure.Net;

public class SaveObject
{
    public int Id { get; set; }
    public int T { get; set; }
    public int P { get; set; }
    public List<int> C { get; set; } = [];
    public string A { get; set; }
    public int AX { get; set; }

    public List<double> N { get; set; } = [];

    public List<string> S { get; set; } = [];

    public Object Object { get; }

    public SaveObject()
    {

    }

    public SaveObject(Object obj)
    {
        Object = obj;
    }
}
