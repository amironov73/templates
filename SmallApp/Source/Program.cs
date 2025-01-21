// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

// ReSharper disable CheckNamespace

#region Using directives

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#endregion

namespace SmallApp;

internal class Program
{
    public static void Main (string[] args)
    {
        var host = CreateAppHost (args);

        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation ("Ready to run");

        try
        {
            host.RunAsync().GetAwaiter().GetResult();
            logger.LogInformation ("Goodbye");
        }
        catch (Exception ex)
        {
            logger.LogCritical (ex, "An exception occured");
        }
    }

    private static IHost CreateAppHost (string[] args)
    {
        var builder = Host.CreateApplicationBuilder (args);
        builder.Services.AddOptions();
        builder.Services.AddLocalization();

        builder.Configuration.AddJsonFile ("appsettings.json",
            optional: false, reloadOnChange: true);
        builder.Configuration.AddEnvironmentVariables();
        builder.Configuration.AddCommandLine (args);

        // builder.Services.Configure<SomeSettings>
        //     (builder.Configuration.GetSection ("SomeSettings"));

        // builder.Services.AddSingleton<ISingleton, SomeSingleton>();
        // builder.Services.AddScoped<IScopedService, SomeScopedService>();
        // builder.Services.AddTransient<ITransientService, SomeTransientService>();

        // builder.Services.AddHostedService<SomeHostedService>();

        return builder.Build();
    }
}
