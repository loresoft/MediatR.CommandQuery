using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Hangfire;
using Hangfire.SqlServer;

using MediatR.CommandQuery.Endpoints;
using MediatR.CommandQuery.Hangfire;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

using Tracker.WebService.Domain;

namespace Tracker.WebService;

public static class Program
{
    private const string OutputTemplate = "{Timestamp:HH:mm:ss.fff} [{Level:u1}] {Message:lj}{NewLine}{Exception}";

    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: OutputTemplate)
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting web host");

            var builder = WebApplication.CreateBuilder(args);

            ConfigureLogging(builder);

            ConfigureServices(builder);

            var app = builder.Build();

            ConfigureMiddleware(app);

            await app.RunAsync();

            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static void ConfigureLogging(WebApplicationBuilder builder)
    {
        string logDirectory = GetLoggingPath();

        builder.Host
            .UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", builder.Environment.ApplicationName)
                .Enrich.WithProperty("EnvironmentName", builder.Environment.EnvironmentName)
                .Filter.ByExcluding(logEvent => logEvent.Exception is OperationCanceledException)
                .WriteTo.Console(outputTemplate: OutputTemplate)
                .WriteTo.File(
                    formatter: new RenderedCompactJsonFormatter(),
                    path: $"{logDirectory}/log.clef",
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 10
                ));
        ;
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddAuthorization();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddProblemDetails(ProblemDetailsCustomizer.Configure);

        services.AddTrackerWebServiceEntityFrameworkCore();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.SerializerOptions.TypeInfoResolverChain.Add(DomainJsonContext.Default);
        });

        // hangfire options
        services.TryAddSingleton(new SqlServerStorageOptions
        {
            PrepareSchemaIfNecessary = true,
            EnableHeavyMigrations = true,
            SqlClientFactory = Microsoft.Data.SqlClient.SqlClientFactory.Instance
        });

        services.AddHangfire((serviceProvider, globalConfiguration) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var stroageOptions = serviceProvider.GetRequiredService<SqlServerStorageOptions>();
            var connectionString = configuration.GetConnectionString("Tracker");

            globalConfiguration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseSqlServerStorage(connectionString, stroageOptions)
                .UseMediatR();
        });

        services.AddMediatorDispatcher();
        services.AddFeatureEndpoints();

        services.AddHangfireServer();

    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapHangfireDashboard("/hangfire");

        app.MapFeatureEndpoints();
    }

    private static string GetLoggingPath()
    {
        // azure home directory
        var homeDirectory = Environment.GetEnvironmentVariable("HOME") ?? ".";
        var logDirectory = Path.Combine(homeDirectory, "LogFiles");

        return Path.GetFullPath(logDirectory);
    }
}
