// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal interface ISuperDataProvider
{
    Task<int> GetNextNumberAsync();
}

internal sealed class SuperDataProvider
    : ISuperDataProvider
{
    private int _nextNumber;
    
    public Task<int> GetNextNumberAsync()
    {
        return Task.FromResult(++_nextNumber);
    }
}

internal sealed class ConsoleGreeter 
    : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly ISuperDataProvider _provider;

    public ConsoleGreeter
        (
            ILogger<ConsoleGreeter> logger,
            IHostApplicationLifetime lifetime,
            ISuperDataProvider provider
        )
    {
        _logger = logger;
        _lifetime = lifetime;
        _provider = provider;
    }

    protected override async Task ExecuteAsync
        (
            CancellationToken stoppingToken
        )
    {
        _logger.LogInformation("Starting");
        
        for (int i = 0; i < 1000; i++)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                return;
            }
            
            var number = await _provider.GetNextNumberAsync();
            await Console.Out.WriteLineAsync($"Doing something: {number}");
            await Task.Delay(10, stoppingToken);
        }

        _logger.LogInformation("Stopping");
        _lifetime.StopApplication();
    }
}

internal sealed class Program
{
    static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<ISuperDataProvider, SuperDataProvider>();
                services.AddHostedService<ConsoleGreeter>();
            });
        
        using (var host = builder.Build())
        {
            host.Start();
            
            host.WaitForShutdown();
        }

        Console.WriteLine("ALL DONE");
    }
}
