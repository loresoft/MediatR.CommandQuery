using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR.CommandQuery.Audit
{
    public static class AuditServiceExtensions
    {
        public static IServiceCollection AddEntityAudit(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IChangeCollector<,>), typeof(ChangeCollector<,>));

            return services;
        }
    }
}
