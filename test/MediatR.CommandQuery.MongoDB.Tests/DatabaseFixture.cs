using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Abstracts;
using KickStart;
using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;
using MediatR.CommandQuery.MongoDB.Tests.Logging;
using MediatR.CommandQuery.Definitions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using MongoDB.Driver;

namespace MediatR.CommandQuery.MongoDB.Tests
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
            ConnectionString = Configuration.GetConnectionString(ConnectionName);

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

            services.AddSingleton(s => MongoFactory.GetDatabaseFromConnectionString(ConnectionString));
            services.AddSingleton(typeof(IMongoEntityRepository<>), typeof(MongoEntityRepository<>));

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
            var priorityRepository = ServiceProvider.GetRequiredService<IMongoEntityRepository<Priority>>();
            priorityRepository.Upsert(Constants.PriorityConstants.High);
            priorityRepository.Upsert(Constants.PriorityConstants.Normal);
            priorityRepository.Upsert(Constants.PriorityConstants.Low);

            var statusRepository = ServiceProvider.GetRequiredService<IMongoEntityRepository<Status>>();
            statusRepository.Upsert(Constants.StatusConstants.NotStarted);
            statusRepository.Upsert(Constants.StatusConstants.InProgress);
            statusRepository.Upsert(Constants.StatusConstants.Completed);
            statusRepository.Upsert(Constants.StatusConstants.Blocked);
            statusRepository.Upsert(Constants.StatusConstants.Deferred);
            statusRepository.Upsert(Constants.StatusConstants.Done);

            var tenantRepository = ServiceProvider.GetRequiredService<IMongoEntityRepository<Tenant>>();
            tenantRepository.Upsert(Constants.TenantConstants.Test);

            var userRepository = ServiceProvider.GetRequiredService<IMongoEntityRepository<User>>();
            userRepository.Upsert(Constants.UserConstants.WilliamAdama);
            userRepository.Upsert(Constants.UserConstants.LauraRoslin);
            userRepository.Upsert(Constants.UserConstants.KaraThrace);
            userRepository.Upsert(Constants.UserConstants.LeeAdama);
            userRepository.Upsert(Constants.UserConstants.GaiusBaltar);
            userRepository.Upsert(Constants.UserConstants.SaulTigh);
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
