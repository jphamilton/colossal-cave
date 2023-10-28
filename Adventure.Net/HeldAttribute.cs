using System;

namespace Adventure.Net;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
public class HeldAttribute : Attribute
{
}
