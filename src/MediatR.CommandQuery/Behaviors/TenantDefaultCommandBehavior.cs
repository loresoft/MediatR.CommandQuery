using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors
{
    public class TenantDefaultCommandBehavior<TKey, TEntityModel, TResponse>
        : PipelineBehaviorBase<EntityModelCommand<TEntityModel, TResponse>, TResponse>
        where TEntityModel : class
    {
        private readonly ITenantResolver<TKey> _tenantResolver;


        public TenantDefaultCommandBehavior(ILoggerFactory loggerFactory, ITenantResolver<TKey> tenantResolver) : base(loggerFactory)
        {
            _tenantResolver = tenantResolver;
        }

        protected override async Task<TResponse> Process(EntityModelCommand<TEntityModel, TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            await SetTenantId(request).ConfigureAwait(false);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }

        private async Task SetTenantId(EntityModelCommand<TEntityModel, TResponse> request)
        {
            if (!(request.Model is IHaveTenant<TKey> tenantModel))
                return;

            if (!Equals(tenantModel.TenantId, default(TKey)))
                return;

            var tenantId = await _tenantResolver.GetTenantId(request.Principal);
            tenantModel.TenantId = tenantId;
        }
    }
}