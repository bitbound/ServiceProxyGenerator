using Bitbound.Blazor.ServiceProxyGenerator.Internals;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace Bitbound.Blazor.ServiceProxyGenerator.Extensions;

public static class IServiceCollectionExtensions
{
    private readonly static Interceptor _interceptor = new();
    private readonly static ProxyGenerator _proxyGenerator = new();

    public static IServiceCollection AddContextAwareKeyedScoped<TService>(
        this IServiceCollection services,
        RenderContext renderContext,
        object serviceKey)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedScoped<TService>(serviceKey);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddKeyedScoped(serviceKey, (_, _) => proxy);
        }
        return services;
    }
    public static IServiceCollection AddContextAwareKeyedScoped<TService, TImplementation>(
        this IServiceCollection services,
        RenderContext renderContext,
        object serviceKey)
        where TService : class
        where TImplementation : class, TService
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedScoped<TService, TImplementation>(serviceKey);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddKeyedScoped(serviceKey, (_, _) => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareKeyedScoped<TService>(
        this IServiceCollection services,
        RenderContext renderContext,
        object serviceKey,
        Func<IServiceProvider, object?, TService> implementationFactory)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedScoped(serviceKey, implementationFactory);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddKeyedScoped(serviceKey, (_, _) => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareKeyedSingleton<TService>(
       this IServiceCollection services,
       RenderContext renderContext,
       object serviceKey)
       where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedSingleton<TService>(serviceKey);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddKeyedSingleton(serviceKey, (_, _) => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareKeyedSingleton<TService, TImplementation>(
        this IServiceCollection services,
        RenderContext renderContext,
        object serviceKey)
        where TService : class
        where TImplementation : class, TService
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedSingleton<TService, TImplementation>(serviceKey);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddKeyedSingleton(serviceKey, (_, _) => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareKeyedSingleton<TService>(
        this IServiceCollection services,
        RenderContext renderContext,
        object serviceKey,
        Func<IServiceProvider, object?, TService> implementationFactory)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedSingleton(serviceKey, implementationFactory);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddKeyedSingleton(serviceKey, (_, _) => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareKeyedTransient<TService>(
        this IServiceCollection services,
        RenderContext renderContext,
        object serviceKey)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedTransient<TService>(serviceKey);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddKeyedTransient(serviceKey, (_, _) => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareKeyedTransient<TService, TImplementation>(
        this IServiceCollection services,
        RenderContext renderContext,
        object serviceKey)
        where TService : class
        where TImplementation : class, TService
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedTransient<TService, TImplementation>(serviceKey);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddKeyedTransient(serviceKey, (_, _) => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareKeyedTransient<TService>(
        this IServiceCollection services,
        RenderContext renderContext,
        object serviceKey,
        Func<IServiceProvider, object?, TService> implementationFactory)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedTransient(serviceKey, implementationFactory);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddKeyedTransient(serviceKey, (_, _) => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareScoped<TService>(
        this IServiceCollection services,
        RenderContext renderContext)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddScoped<TService>();
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddScoped(_ => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareScoped<TService, TImplementation>(
        this IServiceCollection services,
        RenderContext renderContext)
        where TService : class
        where TImplementation : class, TService
    {
        if (RegisterActual(renderContext))
        {
            services.AddScoped<TService, TImplementation>();
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddScoped(_ => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareScoped<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory,
        RenderContext renderContext)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddScoped(implementationFactory);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddScoped(_ => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareSingleton<TService>(
                                                        this IServiceCollection services,
    RenderContext renderContext)
    where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddSingleton<TService>();
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddSingleton(_ => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareSingleton<TService, TImplementation>(
        this IServiceCollection services,
        RenderContext renderContext)
        where TService : class
        where TImplementation : class, TService
    {
        if (RegisterActual(renderContext))
        {
            services.AddSingleton<TService, TImplementation>();
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddSingleton(_ => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareSingleton<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory,
        RenderContext renderContext)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddSingleton(implementationFactory);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddSingleton(_ => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareTransient<TService>(
        this IServiceCollection services,
        RenderContext renderContext)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddTransient<TService>();
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddTransient(_ => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareTransient<TService, TImplementation>(
        this IServiceCollection services,
        RenderContext renderContext)
        where TService : class
        where TImplementation : class, TService
    {
        if (RegisterActual(renderContext))
        {
            services.AddTransient<TService, TImplementation>();
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddTransient(_ => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareTransient<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory,
        RenderContext renderContext)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddTransient(implementationFactory);
        }
        else
        {
            var proxy = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(_interceptor);
            services.AddTransient(_ => proxy);
        }
        return services;
    }

    private static bool RegisterActual(RenderContext renderContext)
    {
        return OperatingSystem.IsBrowser() ?
            renderContext == RenderContext.Browser :
            renderContext == RenderContext.Server;
    }

    private class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException(
                "This service is a stub and has no implementation.  " +
                "Make sure you are in the correct Blazor render context (e.g. server/WASM).");
        }
    }
}
