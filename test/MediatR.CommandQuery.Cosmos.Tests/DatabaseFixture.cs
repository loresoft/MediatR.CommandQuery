using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cosmos.Abstracts;
using KickStart;
using MediatR.CommandQuery.Cosmos.Tests.Data.Entities;
using MediatR.CommandQuery.Cosmos.Tests.Logging;
using MediatR.CommandQuery.Definitions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace MediatR.CommandQuery.Cosmos.Tests
{
    public class DatabaseFixture : IDisposable
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
            services.AddSingleton<IConfiguration>(p => Configuration);
            services.AddLogging(f =>
            {
                f.SetMinimumLevel(LogLevel.Trace);
                f.AddMemory();
                f.AddFilter("Microsoft", LogLevel.Information);
                f.AddFilter("System", LogLevel.Warning);
            });
            services.AddOptions();

            services.TryAddTransient<ITenantResolver<string>, MockTenantResolver>();

            services.AddCosmosRepository();
            services.AddAutoMapper(typeof(DatabaseFixture).Assembly);
            services.AddMediator();
            services.AddValidatorsFromAssembly<DatabaseFixture>();

            services.KickStart(config => config
                .IncludeAssemblyFor<DatabaseFixture>()
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
            var priorityRepository = ServiceProvider.GetRequiredService<ICosmosRepository<Priority>>();
            var statusRepository = ServiceProvider.GetRequiredService<ICosmosRepository<Status>>();
            var tenantRepository = ServiceProvider.GetRequiredService<ICosmosRepository<Tenant>>();
            var userRepository = ServiceProvider.GetRequiredService<ICosmosRepository<User>>();

            System.Threading.Tasks.Task.WaitAll(
               priorityRepository.SaveAsync(Constants.PriorityConstants.High),
               priorityRepository.SaveAsync(Constants.PriorityConstants.Normal),
               priorityRepository.SaveAsync(Constants.PriorityConstants.Low),

               statusRepository.SaveAsync(Constants.StatusConstants.NotStarted),
               statusRepository.SaveAsync(Constants.StatusConstants.InProgress),
               statusRepository.SaveAsync(Constants.StatusConstants.Completed),
               statusRepository.SaveAsync(Constants.StatusConstants.Blocked),
               statusRepository.SaveAsync(Constants.StatusConstants.Deferred),
               statusRepository.SaveAsync(Constants.StatusConstants.Done),

                tenantRepository.SaveAsync(Constants.TenantConstants.Test),

                userRepository.SaveAsync(Constants.UserConstants.WilliamAdama),
                userRepository.SaveAsync(Constants.UserConstants.LauraRoslin),
                userRepository.SaveAsync(Constants.UserConstants.KaraThrace),
                userRepository.SaveAsync(Constants.UserConstants.LeeAdama),
                userRepository.SaveAsync(Constants.UserConstants.GaiusBaltar),
                userRepository.SaveAsync(Constants.UserConstants.SaulTigh)
            );
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
    }
}