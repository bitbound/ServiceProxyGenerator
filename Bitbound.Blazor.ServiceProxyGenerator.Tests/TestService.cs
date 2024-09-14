namespace Bitbound.Blazor.ServiceProxyGenerator.Tests;

public interface ITestService
{
    string GetValue();
    Task SetValue(string value);
    IAsyncEnumerable<int> ReadNumbers();
    Task<int> QueryNumber();
}

public class TestService : ITestService
{
    public string GetValue()
    {
        return "Ok";
    }

    public Task<int> QueryNumber()
    {
        return Task.FromResult(42);
    }

    public async IAsyncEnumerable<int> ReadNumbers()
    {
        for (var i = 0; i < 10; i++)
        {
            yield return i;
            await Task.Yield();
        }
    }

    public Task SetValue(string value)
    {
        return Task.CompletedTask;
    }
}
