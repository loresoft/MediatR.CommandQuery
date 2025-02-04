using Injectio.Attributes;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Abstracts;

using Tracker.WebService.Data.Repositories;

namespace Tracker.WebService.Data;

public class DataServiceRegistration
{
    [RegisterServices]
    public void Register(IServiceCollection services)
    {
        services.AddMongoRepository("Tracker");

        services.AddSingleton<AuditRepository>();
        services.AddSingleton<PriorityRepository>();
        services.AddSingleton<RoleRepository>();
        services.AddSingleton<StatusRepository>();
        services.AddSingleton<TaskRepository>();
        services.AddSingleton<UserRepository>();
        services.AddSingleton<UserLoginRepository>();
    }
}
