namespace TeleApp;

internal /* нельзя static */ sealed class Program
{
    public static void Main (string[] args)
    {
        var builder = WebApplication.CreateBuilder (args);
        var configuration = builder.Configuration;
        configuration.AddEnvironmentVariables ();
        configuration.AddJsonFile ("appsettings.json", optional: true);
        configuration.AddCommandLine (args);

        var services = builder.Services;
        services.AddOptions();
        services.AddMemoryCache();

        Telemetry.AddTelemetryServices (builder);

        var app = builder.Build();

        Telemetry.UseTelemetryServices (app);

        app.MapGet ("/hello", (Telemetry telemetry, ILogger<Program> logger) =>
        {
            telemetry.HelloSaid();

            using (var cacheActivity = telemetry.StartActivity ("cache-get"))
            {
                logger.LogInformation ("Getting value from cache: {Key}", "key1");

                cacheActivity.SetTag ("hello.key1", "value1");
                cacheActivity.SetTag ("hello.key2", "value2");
                SomeDelay();
            }

            using (var databaseActivity = telemetry.StartActivity ("database"))
            {
                logger.LogInformation ("Getting value from database: {Key}", "key1");

                databaseActivity.SetTag ("hello.key1", "value3");
                databaseActivity.SetTag ("hello.key2", "value4");
                SomeDelay();

                logger.LogInformation ("Got value from database: {Value}", "value1");
            }

            using (var cacheActivity = telemetry.StartActivity ("cache-set"))
            {
                logger.LogInformation ("Setting value to cache: {Key}, {Value}",
                    "key1", "value1");

                cacheActivity.SetTag ("hello.key1", "value5");
                cacheActivity.SetTag ("hello.key2", "value6");
                SomeDelay();
            }

            return "Hello World";
        });

        app.MapGet ("/goodbye", (Telemetry telemetry) =>
        {
            telemetry.HelloSaid();
            SomeDelay();

            return "Goodbye World";
        });

        app.Run();
    }

    private static void SomeDelay() =>
        Thread.Sleep (Random.Shared.Next (100, 300));
}
