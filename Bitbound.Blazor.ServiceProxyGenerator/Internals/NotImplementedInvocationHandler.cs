using System.Reflection;

namespace Bitbound.Blazor.ServiceProxyGenerator.Internals;

internal sealed class NotImplementedInvocationHandler : IInvocationHandler
{
    public object? Invoke(MethodInfo method, object[]? args)
    {
        throw new NotImplementedException(
            "This service is a stub and has no implementation.  " +
            "Make sure you are in the correct Blazor render context (e.g. server/WASM).");
    }
}
