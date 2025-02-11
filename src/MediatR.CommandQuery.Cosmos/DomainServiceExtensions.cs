using Cosmos.Abstracts;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Cosmos.Handlers;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR.CommandQuery.Cosmos;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddEntityQueries<TRepository, TEntity, TReadModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TReadModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        // standard queries
        services.TryAddTransient<IRequestHandler<EntityIdentifierQuery<string, TReadModel>, TReadModel>, EntityIdentifierQueryHandler<TRepository, TEntity, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntityIdentifiersQuery<string, TReadModel>, IReadOnlyCollection<TReadModel>>, EntityIdentifiersQueryHandler<TRepository, TEntity, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, EntityPagedQueryHandler<TRepository, TEntity, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, EntitySelectQueryHandler<TRepository, TEntity, TReadModel>>();

        services.AddEntityQueryBehaviors<string, TReadModel>();

        return services;
    }


    public static IServiceCollection AddEntityCommands<TRepository, TEntity, TReadModel, TCreateModel, TUpdateModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TCreateModel : class
        where TUpdateModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddEntityCreateCommand<TRepository, TEntity, TReadModel, TCreateModel>()
            .AddEntityUpdateCommand<TRepository, TEntity, TReadModel, TUpdateModel>()
            .AddEntityUpsertCommand<TRepository, TEntity, TReadModel, TUpdateModel>()
            .AddEntityPatchCommand<TRepository, TEntity, TReadModel>()
            .AddEntityDeleteCommand<TRepository, TEntity, TReadModel>();

        return services;
    }


    public static IServiceCollection AddEntityCreateCommand<TRepository, TEntity, TReadModel, TCreateModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TCreateModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, EntityCreateCommandHandler<TRepository, TEntity, TCreateModel, TReadModel>>();

        // pipeline registration, run in order registered
        services.AddEntityCreateBehaviors<string, TReadModel, TCreateModel>();

        return services;
    }

    public static IServiceCollection AddEntityUpdateCommand<TRepository, TEntity, TReadModel, TUpdateModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TUpdateModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        // allow query for update models
        services.TryAddTransient<IRequestHandler<EntityIdentifierQuery<string, TUpdateModel>, TUpdateModel>, EntityIdentifierQueryHandler<TRepository, TEntity, TUpdateModel>>();
        services.TryAddTransient<IRequestHandler<EntityIdentifiersQuery<string, TUpdateModel>, IReadOnlyCollection<TUpdateModel>>, EntityIdentifiersQueryHandler<TRepository, TEntity, TUpdateModel>>();

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityUpdateCommand<string, TUpdateModel, TReadModel>, TReadModel>, EntityUpdateCommandHandler<TRepository, TEntity, TUpdateModel, TReadModel>>();

        services.AddEntityUpdateBehaviors<string, TReadModel, TUpdateModel>();

        return services;
    }

    public static IServiceCollection AddEntityUpsertCommand<TRepository, TEntity, TReadModel, TUpdateModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TUpdateModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityUpsertCommand<string, TUpdateModel, TReadModel>, TReadModel>, EntityUpsertCommandHandler<TRepository, TEntity, TUpdateModel, TReadModel>>();

        services.AddEntityUpsertBehaviors<string, TReadModel, TUpdateModel>();

        return services;
    }

    public static IServiceCollection AddEntityPatchCommand<TRepository, TEntity, TReadModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
    {
        ArgumentNullException.ThrowIfNull(services);

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityPatchCommand<string, TReadModel>, TReadModel>, EntityPatchCommandHandler<TRepository, TEntity, TReadModel>>();

        services.AddEntityPatchBehaviors<string, TEntity, TReadModel>();

        return services;
    }

    public static IServiceCollection AddEntityDeleteCommand<TRepository, TEntity, TReadModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
    {
        ArgumentNullException.ThrowIfNull(services);

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityDeleteCommand<string, TReadModel>, TReadModel>, EntityDeleteCommandHandler<TRepository, TEntity, TReadModel>>();

        services.AddEntityDeleteBehaviors<string, TEntity, TReadModel>();

        return services;
    }
}
