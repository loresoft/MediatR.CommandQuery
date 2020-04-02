using System;
using System.Collections.Generic;
using EntityChange;

namespace MediatR.CommandQuery.Audit
{
    /// <summary>
    /// An audit record of changes for an entity
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class AuditRecord<TKey>
    {
        /// <summary>
        /// Gets or sets the key for the entity.
        /// </summary>
        /// <value>
        /// The key for the entity.
        /// </value>
        public TKey Key { get; set; }

        /// <summary>
        /// Gets or sets the entity name.
        /// </summary>
        /// <value>
        /// The entity name.
        /// </value>
        public string Entity { get; set; }

        /// <summary>
        /// Gets or sets the description of the entity.
        /// </summary>
        /// <value>
        /// The description of the entity.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the date the change activity occured.
        /// </summary>
        /// <value>
        /// The date the change activity occured.
        /// </value>
        public DateTimeOffset ActivityDate { get; set; }

        /// <summary>
        /// Gets or sets the username that initiated the activity.
        /// </summary>
        /// <value>
        /// The the username that initiated the activity.
        /// </value>
        public string ActivityBy { get; set; }

        /// <summary>
        /// Gets or sets the type of operation.
        /// </summary>
        /// <value>
        /// The type of operation.
        /// </value>
        public AuditOperation Operation { get; set; }

        /// <summary>
        /// Gets or sets the list of changes.
        /// </summary>
        /// <value>
        /// The list of changes.
        /// </value>
        public IReadOnlyCollection<ChangeRecord> Changes { get; set; }
    }
}