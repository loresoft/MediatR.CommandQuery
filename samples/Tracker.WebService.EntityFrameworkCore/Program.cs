using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using MediatR.CommandQuery.Endpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Events;

using Tracker.WebService.Domain;

namespace Tracker.WebService;

public static class Program
{
    private const string OutputTemplate = "{Timestamp:HH:mm:ss.fff} [{Level:u1}] {Message:lj}{NewLine}{Exception}";

    public static async Task<int> Main(string[] args)
    {
        // azure home directory
        var homeDirectory = Environment.GetEnvironmentVariable("HOME") ?? ".";
        var logDirectory = Path.Combine(homeDirectory, "LogFiles");

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

            builder.Host
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", builder.Environment.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", builder.Environment.EnvironmentName)
                    .WriteTo.Console(outputTemplate: OutputTemplate)
                    .WriteTo.File(
                        path: $"{logDirectory}/log.txt",
                        rollingInterval: RollingInterval.Day,
                        shared: true,
                        flushToDiskInterval: TimeSpan.FromSeconds(1),
                        outputTemplate: OutputTemplate,
                        retainedFileCountLimit: 10
                    )
                );

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

        app.MapFeatureEndpoints();
    }
}
