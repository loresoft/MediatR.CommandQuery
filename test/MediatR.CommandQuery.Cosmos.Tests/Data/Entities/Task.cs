using System;
using Cosmos.Abstracts;
using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Cosmos.Tests.Data.Entities
{
    public class Task : CosmosEntity,  IHaveIdentifier<string>, IHaveTenant<string>, ITrackCreated, ITrackUpdated
    {
        public string StatusId { get; set; }

        public string PriorityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? DueDate { get; set; }

        public DateTimeOffset? CompleteDate { get; set; }

        public string AssignedId { get; set; }

        [PartitionKey]
        public string TenantId { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Created { get; set; }

        public string UpdatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

    }
}
