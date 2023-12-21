using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Audit;

/// <summary>
/// Interface defining an entity change service
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IChangeCollector<TKey, TEntity>
    where TEntity : IHaveIdentifier<TKey>, ITrackUpdated, ITrackHistory
{
    /// <summary>
    /// Collects the changes to an entity over time by comparing the historical version.
    /// </summary>
    /// <param name="entities">The historical list of an entity.</param>
    /// <param name="entityName">Name of the entity used in the log record.</param>
    /// <param name="descriptionFunction">The entity description function.</param>
    /// <returns>A list of audit record of changes to the entity</returns>
    IEnumerable<AuditRecord<TKey>> CollectChanges(IEnumerable<TEntity> entities, string entityName, Func<TEntity, string>? descriptionFunction = null);

    /// <summary>
    /// Collects the changes to a group of entities over time by comparing the historical version.
    /// </summary>
    /// <param name="entities">The historical list of an entity.</param>
    /// <param name="entityName">Name of the entity used in the log record.</param>
    /// <param name="descriptionFunction">The entity description function.</param>
    /// <returns>
    /// A list of audit record of changes to the entity
    /// </returns>
    IReadOnlyList<AuditRecord<TKey>> CollectGroupChanges(IEnumerable<TEntity> entities, string entityName, Func<TEntity, string>? descriptionFunction = null);

    /// <summary>
    /// Collects the changes to a group of entities over time by comparing the historical version.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property to group by.</typeparam>
    /// <param name="entities">The historical list of an entity.</param>
    /// <param name="entityName">Name of the entity used in the log record.</param>
    /// <param name="groupSelector">The group selector.</param>
    /// <param name="descriptionFunction">The entity description function.</param>
    /// <returns>
    /// A list of audit record of changes to the entity
    /// </returns>
    IReadOnlyList<AuditRecord<TKey>> CollectGroupChanges<TProperty>(IEnumerable<TEntity> entities, string entityName, Func<TEntity, TProperty> groupSelector, Func<TEntity, string>? descriptionFunction = null);
}
