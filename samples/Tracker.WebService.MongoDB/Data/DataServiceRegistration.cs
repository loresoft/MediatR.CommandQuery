using System.Collections.Generic;

using KickStart.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Data.Repositories;

namespace Tracker.WebService.Data
{
    public class DataServiceRegistration : IDependencyInjectionRegistration
    {
        public void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                return MongoFactory.GetDatabaseFromConnectionString(configuration.GetConnectionString("Tracker"));
            });

            services.AddSingleton(typeof(IMongoEntityRepository<>), typeof(MongoEntityRepository<>));

            services.AddSingleton<AuditRepository>();
            services.AddSingleton<PriorityRepository>();
            services.AddSingleton<RoleRepository>();
            services.AddSingleton<StatusRepository>();
            services.AddSingleton<TaskRepository>();
            services.AddSingleton<UserRepository>();
            services.AddSingleton<UserLoginRepository>();
        }
    }
}
