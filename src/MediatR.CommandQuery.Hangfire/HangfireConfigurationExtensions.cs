using System;
using System.ComponentModel;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

namespace MediatR.CommandQuery.Hangfire
{
    public static class HangfireConfigurationExtensions
    {
        public static IGlobalConfiguration UseMediatR(this IGlobalConfiguration configuration)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            configuration.UseSerializerSettings(jsonSettings);

            return configuration;
        }

        public static IServiceCollection AddMediatorDispatcher(this IServiceCollection services)
        {
            services.TryAddScoped<IMediatorDispatcher, MediatorDispatcher>();

            return services;
        }
    }
}
