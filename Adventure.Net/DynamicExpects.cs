using System;
using System.Reflection;

namespace Adventure.Net;

public class DynamicExpects : IInvoke
{
    private readonly Type verbType;
    private readonly DynamicCall call;

    public MethodInfo Expects { get; }

    public DynamicExpects(Verb verb, MethodInfo handler, DynamicCall call)
    {
        this.call = call;
        this.call = call;

        verbType = verb.GetType();

        Expects = handler;
    }

    public bool Invoke()
    {
        var invoker = new Invoker();

        if (Expects != null)
        {
            invoker.Add(new DymamicInvoke(Expects, verbType, call.Args));

            return invoker.Invoke();
        }

        return false;
    }
}
