using EntityChange;
using EntityChange.Extensions;

using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Audit;

/// <summary>
/// Service to collate changes for an entity
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="MediatR.CommandQuery.Audit.IChangeCollector{TKey, TEntity}" />
public partial class ChangeCollector<TKey, TEntity> : IChangeCollector<TKey, TEntity>
    where TEntity : IHaveIdentifier<TKey>, ITrackUpdated, ITrackHistory
{
    private readonly IEntityComparer _entityComparer;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeCollector{TKey, TEntity}"/> class.
    /// </summary>
    /// <param name="entityComparer">The entity comparer.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    public ChangeCollector(IEntityComparer entityComparer, ILoggerFactory loggerFactory)
    {
        _entityComparer = entityComparer;
        _logger = loggerFactory.CreateLogger<ChangeCollector<TKey, TEntity>>();
    }

    /// <summary>
    /// Collects the changes to a group of entities over time by comparing the historical version.
    /// </summary>
    /// <param name="entities">The historical list of an entity.</param>
    /// <param name="entityName">Name of the entity used in the log record.</param>
    /// <param name="descriptionFunction">The entity description function.</param>
    /// <returns>
    /// A list of audit record of changes to the entity
    /// </returns>
    public IReadOnlyList<AuditRecord<TKey>> CollectGroupChanges(IEnumerable<TEntity> entities, string entityName, Func<TEntity, string>? descriptionFunction = null)
    {
        return CollectGroupChanges(entities, entityName, g => g.Id, descriptionFunction);
    }

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
    public IReadOnlyList<AuditRecord<TKey>> CollectGroupChanges<TProperty>(IEnumerable<TEntity> entities, string entityName, Func<TEntity, TProperty> groupSelector, Func<TEntity, string>? descriptionFunction = null)
    {
        if (entities is null)
            throw new ArgumentNullException(nameof(entities));
        if (entityName is null)
            throw new ArgumentNullException(nameof(entityName));
        if (entityName.Length == 0)
            throw new ArgumentException("Invalid entity name", nameof(entityName));
        if (groupSelector is null)
            throw new ArgumentNullException(nameof(groupSelector));

        var historyList = new List<AuditRecord<TKey>>();

        foreach (var group in entities.GroupBy(groupSelector))
        {
            try
            {
                LogCollectingChanges(_logger, entityName, group.Key);

                var groupList = group
                    .OrderBy(p => p.PeriodEnd)
                    .ToList();

                var auditList = CollectChanges(groupList, entityName, descriptionFunction);
                historyList.AddRange(auditList);
            }
            catch (Exception ex)
            {
                LogCollectingError(_logger, entityName, group.Key, ex.Message, ex);

                throw;
            }
        }

        return historyList;
    }

    /// <summary>
    /// Collects the changes to an entity over time by comparing the historical version.
    /// </summary>
    /// <param name="entities">The historical list of an entity.</param>
    /// <param name="entityName">Name of the entity used in the log record.</param>
    /// <param name="descriptionFunction">The entity description function.</param>
    /// <returns>
    /// A list of audit record of changes to the entity
    /// </returns>
    /// <exception cref="ArgumentNullException">When entities or entityName is null</exception>
    /// <exception cref="ArgumentException">When entity name is empty</exception>
    public IEnumerable<AuditRecord<TKey>> CollectChanges(IEnumerable<TEntity> entities, string entityName, Func<TEntity, string>? descriptionFunction = null)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));
        if (entityName == null)
            throw new ArgumentNullException(nameof(entityName));
        if (entityName.Length == 0)
            throw new ArgumentException("Invalid entity name", nameof(entityName));

        // make sure in historical order
        var entityList = entities
            .OrderBy(p => p.PeriodEnd)
            .ToList();

        if (entityList.Count == 0)
            return Enumerable.Empty<AuditRecord<TKey>>();

        var historyRecords = new List<AuditRecord<TKey>>();

        TEntity? previous = default;
        int index = 0;

        var displayName = entityName.ToSpacedWords();

        foreach (var current in entityList)
        {
            bool isLast = index == entityList.Count - 1;
            List<ChangeRecord>? changes = null;

            if (previous != null)
            {
                changes = _entityComparer
                    .Compare(previous, current)
                    .ToList();
            }

            var description = descriptionFunction?.Invoke(current) ?? current.ToString();

            var auditRecord = new AuditRecord<TKey>
            {
                Key = current.Id,
                Entity = entityName,
                DisplayName = displayName,
                Description = description,
                ActivityBy = current.UpdatedBy,
                Changes = changes
            };

            if (isLast && current.PeriodEnd < DateTime.MaxValue)
            {
                auditRecord.ActivityDate = new DateTimeOffset(current.PeriodEnd, TimeSpan.Zero);
                auditRecord.Operation = AuditOperation.Delete;
            }
            else if (previous == null)
            {
                auditRecord.ActivityDate = new DateTimeOffset(current.PeriodStart, TimeSpan.Zero);
                auditRecord.Operation = AuditOperation.Create;
            }
            else
            {
                auditRecord.ActivityDate = new DateTimeOffset(current.PeriodStart, TimeSpan.Zero);
                auditRecord.Operation = AuditOperation.Update;
            }

            historyRecords.Add(auditRecord);

            previous = current;
            index++;
        }

        return historyRecords;
    }


    [LoggerMessage(1, LogLevel.Debug, "Collecting changes for {EntityName} with key {EntityKey} ...")]
    static partial void LogCollectingChanges(ILogger logger, string entityName, object entityKey);

    [LoggerMessage(2, LogLevel.Error, "Error collecting changes for {EntityName} with key {EntityKey}: {ErrorMessage}")]
    static partial void LogCollectingError(ILogger logger, string entityName, object entityKey, string errorMessage, Exception? exception);

}
