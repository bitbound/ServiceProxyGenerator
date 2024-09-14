using System.Reflection.Emit;
using System.Reflection;
using Bitbound.Blazor.ServiceProxyGenerator.Exceptions;

namespace Bitbound.Blazor.ServiceProxyGenerator.Internals;

internal static class ProxyServiceGenerator
{
    public static TServiceInterface CreateProxy<TServiceInterface>()
        where TServiceInterface : class
    {
        var handler = new NotImplementedInvocationHandler();
        var interfaceType = typeof(TServiceInterface);
        if (!interfaceType.IsInterface)
            throw new DynamicObjectGenerationException("T must be an interface type.");

        if (handler == null)
            throw new DynamicObjectGenerationException("Handler cannot be null.");

        var assemblyName = new AssemblyName("BlazorProxyAssembly");
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("BlazorProxyModule");

        var typeBuilder = moduleBuilder.DefineType("BlazorProxyType",
            TypeAttributes.Public | TypeAttributes.Class,
            null,
            [interfaceType]);

        var handlerField = typeBuilder.DefineField("_handler", typeof(IInvocationHandler), FieldAttributes.Private);

        // Constructor
        var ctor = typeBuilder.DefineConstructor(
            MethodAttributes.Public,
            CallingConventions.Standard,
            [typeof(IInvocationHandler)]);

        var ctorIL = ctor.GetILGenerator();
        ctorIL.Emit(OpCodes.Ldarg_0);
        ctorIL.Emit(
          OpCodes.Call,
          typeof(object).GetConstructor(Type.EmptyTypes)
            ?? throw new DynamicObjectGenerationException("Object constructor not found."));
        ctorIL.Emit(OpCodes.Ldarg_0);
        ctorIL.Emit(OpCodes.Ldarg_1);
        ctorIL.Emit(OpCodes.Stfld, handlerField);
        ctorIL.Emit(OpCodes.Ret);

        // Implement interface methods
        foreach (var method in interfaceType.GetMethods())
        {
            ImplementMethod(typeBuilder, method, handlerField);
        }

        var proxyType = typeBuilder.CreateType();
        var instance = Activator.CreateInstance(proxyType, handler)
          ?? throw new DynamicObjectGenerationException("Failed to create instance of proxy type.");

        return (TServiceInterface)instance;
    }

    private static void ImplementMethod(TypeBuilder typeBuilder, MethodInfo method, FieldBuilder handlerField)
    {
        var parameters = method.GetParameters();
        var parameterTypes = Array.ConvertAll(parameters, p => p.ParameterType);

        var methodBuilder = typeBuilder.DefineMethod(
            method.Name,
            MethodAttributes.Public | MethodAttributes.Virtual,
            method.ReturnType,
            parameterTypes);

        var methodIL = methodBuilder.GetILGenerator();

        // Load 'this' and handler field
        methodIL.Emit(OpCodes.Ldarg_0);
        methodIL.Emit(OpCodes.Ldfld, handlerField);

        // Load method info
        methodIL.Emit(OpCodes.Ldtoken, method);
        methodIL.Emit(
          OpCodes.Call,
          typeof(MethodBase).GetMethod("GetMethodFromHandle", [typeof(RuntimeMethodHandle)])
            ?? throw new DynamicObjectGenerationException("GetMethodFromHandle method not found."));

        // Create and load parameters array
        methodIL.Emit(OpCodes.Ldc_I4, parameters.Length);
        methodIL.Emit(OpCodes.Newarr, typeof(object));

        for (int i = 0; i < parameters.Length; i++)
        {
            methodIL.Emit(OpCodes.Dup);
            methodIL.Emit(OpCodes.Ldc_I4, i);
            methodIL.Emit(OpCodes.Ldarg, i + 1);
            if (parameterTypes[i].IsValueType)
            {
                methodIL.Emit(OpCodes.Box, parameterTypes[i]);
            }
            methodIL.Emit(OpCodes.Stelem_Ref);
        }

        methodIL.Emit(
         OpCodes.Callvirt,
         typeof(IInvocationHandler)
           .GetMethod(nameof(IInvocationHandler.Invoke))
           ?? throw new DynamicObjectGenerationException("Invoke method not found on proxy implementation."));

        methodIL.Emit(OpCodes.Ret);

        typeBuilder.DefineMethodOverride(methodBuilder, method);
    }
}
