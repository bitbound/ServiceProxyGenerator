# Service Proxy Generator

A dependency injection helper for Blazor that registers service proxies in the non-target render context.

This allows for injecting real implementations when running in the browser, while dynamically-generated
proxies will be injected during server-side prerendering.

## Quick Start (recommended usage)

````C#
// In the Blazor Client project.
public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddClientServices(this IServiceCollection services)
  {
    // Use context-aware overloads for services that are used in the client but
    // will also be loaded during server-side prerendering.
    collection.AddContextAwareTransient<IServiceOne, ServiceOne>(RenderContext.Browser);
    collection.AddContextAwareScoped<IServiceTwo, ServiceTwo>(RenderContext.Browser);
    // ... Add all services this way.
  }
}

```C#
  // In Program.cs in the Blazor Client project;
  builder.Services.AddClientServices();
````

```C#
  // In Program.cs in the Blazor Server project;
  builder.Services.AddClientServices();
```

```razor

  @*
     In MainLayout.razor in the Blazor Client project.
     During prerendering on the server, dynamically-generated proxies
     will be injected for your services.

     When rendered on the client, they will be the real implementions.

     Now you just have to check OperatingSystem.IsBrowser() before using them.
  *@

  @inherits LayoutComponentBase
  @inject IServiceOne ServiceOne
  @inject IServiceTwo ServiceTwo
  @inject ILogger<MainLayout> Logger

  <MudThemeProvider />
  <MudPopoverProvider />
  <MudDialogProvider />
  <MudSnackbarProvider />

  <MudLayout>
    @* Body of layout *@
  </MudLayout>

  @code {
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (OperatingSystem.IsBrowser())
        {
            // Use ServiceOne and ServiceTwo here.
        }
    }
  }
```
