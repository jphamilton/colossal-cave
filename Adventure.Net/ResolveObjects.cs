using System.Collections.Generic;

namespace Adventure.Net;

/// <summary>
/// The parser will resolve objects and forward them to the verb
/// </summary>
public abstract class ResolveObjects : Verb
{
    public abstract bool Handle(List<Object> objects);
}
