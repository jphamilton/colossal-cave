using System;
using System.Reflection;

namespace Adventure.Net;

internal class DymamicInvoke : IInvoke
{
    private readonly MethodInfo expects;
    private readonly Type verbType;
    private readonly object[] args;

    public DymamicInvoke(MethodInfo expects, Type verbType, object[] args)
    {
        this.expects = expects;
        this.verbType = verbType;
        this.args = args;
    }

    public bool Invoke()
    {
        var instance = Activator.CreateInstance(verbType);
        return (bool)expects.Invoke(instance, args);
    }
}
