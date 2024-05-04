using System.Net;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public class TenantAuthenticateCommandBehavior<TKey, TEntityModel, TResponse>
    : PipelineBehaviorBase<EntityModelCommand<TEntityModel, TResponse>, TResponse>
    where TEntityModel : class
{

    private readonly ITenantResolver<TKey> _tenantResolver;

    public TenantAuthenticateCommandBehavior(ILoggerFactory loggerFactory, ITenantResolver<TKey> tenantResolver)
        : base(loggerFactory)
    {
        _tenantResolver = tenantResolver ?? throw new ArgumentNullException(nameof(tenantResolver));
    }

    protected override async Task<TResponse> Process(
        EntityModelCommand<TEntityModel, TResponse> request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (next is null)
            throw new ArgumentNullException(nameof(next));

        await Authorize(request).ConfigureAwait(false);

        // continue pipeline
        return await next().ConfigureAwait(false);
    }

    private async Task Authorize(EntityModelCommand<TEntityModel, TResponse> request)
    {
        var principal = request.Principal;
        if (principal == null)
            return;

        // check principal tenant is same as model tenant
        if (request.Model is not IHaveTenant<TKey> tenantModel)
            return;

        var tenantId = await _tenantResolver.GetTenantId(principal);
        if (Equals(tenantId, tenantModel.TenantId))
            return;

        Logger.LogError("User {UserName} does not have access to specified tenant: {TenantId}", principal.Identity?.Name, tenantId);

        throw new DomainException(HttpStatusCode.Forbidden, "User does not have access to specified tenant.");
    }
}
