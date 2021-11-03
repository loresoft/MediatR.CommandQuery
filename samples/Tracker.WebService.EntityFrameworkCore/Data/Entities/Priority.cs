using System;
using System.Collections.Generic;

using MediatR.CommandQuery.Definitions;

namespace Tracker.WebService.Data.Entities
{
    public partial class Priority : IHaveIdentifier<Guid>, ITrackCreated, ITrackUpdated
    {
        public Priority()
        {
            #region Generated Constructor
            Tasks = new HashSet<Task>();
            #endregion
        }

        #region Generated Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Task> Tasks { get; set; }

        #endregion

    }
}
