﻿using System.Collections.Generic;

namespace Adventure.Net;

/// <summary>
/// Verbs derived from ForwardTokens process command input themselves rather
/// than relying on the parser
/// </summary>
public abstract class ForwardTokens : Verb
{
    public abstract bool Handle(List<string> tokens);
}
