﻿using System.Reflection;

namespace Bitbound.Blazor.ServiceProxyGenerator.Internals;

public interface IInvocationHandler
{
    object? Invoke(MethodInfo method, object[]? args);
}
