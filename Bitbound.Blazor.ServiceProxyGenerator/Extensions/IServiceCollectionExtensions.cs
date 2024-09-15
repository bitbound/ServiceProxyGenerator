using Bitbound.Blazor.ServiceProxyGenerator.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace Bitbound.Blazor.ServiceProxyGenerator.Extensions;

public static class IServiceCollectionExtensions
{

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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
            services.AddKeyedScoped(serviceKey, (_, _) => proxy);
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
            services.AddKeyedSingleton(serviceKey, (_, _) => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareKeyedSingleton<TService, TImplementation>(
        this IServiceCollection services,
        RenderContext renderContext,
        object serviceKey,
        TService instance)
        where TService : class
        where TImplementation : class, TService
    {
        if (RegisterActual(renderContext))
        {
            services.AddKeyedSingleton(serviceKey, instance);
        }
        else
        {
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
            services.AddKeyedSingleton(serviceKey, (_, _) => proxy);
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
            services.AddKeyedTransient(serviceKey, (_, _) => proxy);
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
            services.AddScoped(_ => proxy);
        }
        return services;
    }

    public static IServiceCollection AddContextAwareSingleton<TService>(
        this IServiceCollection services,
        RenderContext renderContext,
        TService instance)
        where TService : class
    {
        if (RegisterActual(renderContext))
        {
            services.AddSingleton(instance);
        }
        else
        {
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
            services.AddSingleton(_ => proxy);
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
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
            var proxy = ProxyServiceGenerator.CreateProxy<TService>();
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
}
