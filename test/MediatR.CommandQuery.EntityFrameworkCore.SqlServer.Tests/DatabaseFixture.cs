using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using DbUp;
using DbUp.Engine.Output;
using KickStart;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Logging;
using MediatR.CommandQuery.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests
{
    public class DatabaseFixture : IUpgradeLog, IDisposable
    {
        private readonly ILogger _logger;

        public DatabaseFixture()
        {
            var enviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Test";
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{enviromentName}.json", true);

            Configuration = builder.Build();
            ConnectionString = Configuration.GetConnectionString(ConnectionName); ;

            var services = new ServiceCollection();
            services.AddSingleton(p => Configuration);
            services.AddLogging(f =>
            {
                f.SetMinimumLevel(LogLevel.Trace);
                f.AddMemory();
                f.AddFilter("Microsoft", LogLevel.Information);
                f.AddFilter("System", LogLevel.Warning);
            });
            services.AddOptions();

            services.TryAddTransient<ITenantResolver<Guid>, MockTenantResolver>();

            services.AddAutoMapper(typeof(TrackerContext).Assembly);
            services.AddMediator();
            services.AddValidatorsFromAssembly<TrackerContext>();

            services.KickStart(config => config
                .IncludeAssemblyFor<TrackerContext>()
                .IncludeAssemblyFor<DomainException>()
                .Data("configuration", Configuration)
                .Data("hostProcess", "test")
                .UseStartupTask()
            );

            ServiceProvider = services.BuildServiceProvider();

            var factory = ServiceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<DatabaseFixture>();

            CreateDatabase();
        }


        public string ConnectionString { get; set; }

        public string ConnectionName { get; set; } = "Tracker";

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ServiceProvider { get; }


        private void CreateDatabase()
        {
            EnsureDatabase.For
                .SqlDatabase(ConnectionString, this);

            var upgradeEngine = DeployChanges.To
                .SqlDatabase(ConnectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogTo(this)
                .Build();

            var result = upgradeEngine.PerformUpgrade();

            if (result.Successful)
                return;

            _logger.LogError(result.Error, "Database Error: {message}", result.Error.Message);

            throw result.Error;
        }


        public void Report(ITestOutputHelper output)
        {
            var logs = MemoryLoggerProvider.Current.LogEntries.ToList();

            foreach (var log in logs)
                output.WriteLine(log.ToString());

            // reset logger
            MemoryLoggerProvider.Current.Clear();
        }

        public void Dispose()
        {
        }


        void IUpgradeLog.WriteInformation(string format, params object[] args)
        {
            _logger.LogInformation(format, args);
        }

        void IUpgradeLog.WriteError(string format, params object[] args)
        {
            _logger.LogError(format, args);
        }

        void IUpgradeLog.WriteWarning(string format, params object[] args)
        {
            _logger.LogWarning(format, args);
        }

    }
}