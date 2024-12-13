using System.Diagnostics;
using System.Diagnostics.Metrics;

using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TeleApp;

internal sealed class Telemetry
{
    /// <summary>
    /// Имя метрики.
    /// </summary>
    public const string ServiceName = "TeleApp";

    /// <summary>
    /// Отслеживание активности.
    /// </summary>
    public ActivitySource Source { get; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public Telemetry
        (
            IMeterFactory factory
        )
    {
        Source = new ActivitySource (ServiceName);
        var meter = factory.Create (ServiceName);

        _helloCounter = meter.CreateCounter<long>
            (
                "tele.hello",
                description: "How many times is hello said"
            );
        _goodbyeCounter = meter.CreateCounter<long>
            (
                "tele.goodbye",
                description: "How many times is goodbye said"
            );
    }

    private readonly Counter<long> _helloCounter;
    private readonly Counter<long> _goodbyeCounter;

    public void HelloSaid() => _helloCounter.Add (1);

    public void GoodbyeSaid() => _goodbyeCounter.Add (1);

    public Activity StartActivity (string activityName)
        => Source.StartActivity (activityName)!;

    /// <summary>
    /// Добавление сервисов телеметрии.
    /// </summary>
    public static void AddTelemetryServices
        (
            WebApplicationBuilder builder
        )
    {
        var exporterUrl = new Uri ("http://localhost:4317");
        var services = builder.Services;

        services.AddSingleton<Telemetry>();
        services.AddOpenTelemetry()
            .ConfigureResource (resource => resource.AddService (ServiceName))

            .WithTracing (tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddSource (ServiceName)
                .AddOtlpExporter (exporter =>
                    exporter.Endpoint = exporterUrl))

            .WithMetrics (metrics => metrics
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddMeter (ServiceName)
                .AddPrometheusExporter()
                .AddOtlpExporter (exporter =>
                    exporter.Endpoint = exporterUrl));

        builder.Logging.AddOpenTelemetry (telemetry => telemetry
            .AddOtlpExporter (exporter =>
            {
                exporter.Endpoint = new Uri("http://localhost:5341/ingest/otlp/v1/logs");
                exporter.Protocol = OtlpExportProtocol.HttpProtobuf;
                exporter.Headers = "X-Seq-ApiKey=FWAzfUOpk7fEguHm3ZJE";
            }));
    }

    /// <summary>
    /// Использование сервисов телеметрии.
    /// </summary>
    public static void UseTelemetryServices
        (
            WebApplication application
        )
    {
        application.MapPrometheusScrapingEndpoint();
    }
}
