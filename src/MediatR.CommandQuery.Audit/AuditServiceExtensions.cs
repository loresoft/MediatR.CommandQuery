using EntityChange;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR.CommandQuery.Audit;

public static class AuditServiceExtensions
{
    public static IServiceCollection AddEntityAudit(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<IEntityConfiguration>(sp => new EntityConfiguration(sp.GetServices<IEntityProfile>()));
        services.TryAddSingleton<IEntityComparer>(sp => new EntityComparer(sp.GetRequiredService<IEntityConfiguration>()));

        services.TryAddSingleton(typeof(IChangeCollector<,>), typeof(ChangeCollector<,>));

        return services;
    }
}
