using Bitbound.Blazor.ServiceProxyGenerator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Bitbound.Blazor.ServiceProxyGenerator.Tests;

public class ProxyServiceTests
{
    [Fact]
    public async Task GivenCorrectRenderContext_WhenServiceIsRequested_RealImplementationIsResolved()
    {
        var collection = new ServiceCollection();
        collection.AddContextAwareTransient<ITestService, TestService>(RenderContext.Server);
        var services = collection.BuildServiceProvider();
        var testService = services.GetRequiredService<ITestService>();

        Assert.Equal("Ok", testService.GetValue());

        var queryResult = await testService.QueryNumber();
        Assert.Equal(42, queryResult);

        var setTask = testService.SetValue("Ok");
        Assert.Equal(Task.CompletedTask, setTask);

        var count = 0;
        await foreach (var value in testService.ReadNumbers())
        {
            count++;
        }
        Assert.Equal(10, count);
    }

    [Fact]
    public void GivenIncorrectRenderContext_WhenServiceIsRequested_ProxyIsResolved()
    {
        var collection = new ServiceCollection();
        collection.AddContextAwareTransient<ITestService, TestService>(RenderContext.Browser);
        var services = collection.BuildServiceProvider();
        var testService = services.GetRequiredService<ITestService>();

        Assert.Throws<NotImplementedException>(() => testService.GetValue());

        Assert.ThrowsAsync<NotImplementedException>(() => testService.QueryNumber());

        Assert.ThrowsAsync<NotImplementedException>(() => testService.SetValue("Ok"));

        Assert.ThrowsAsync<NotImplementedException>(async () =>
        {
            var count = 0;
            await foreach (var number in testService.ReadNumbers())
            {
                count++;
            }
        });
    }
}