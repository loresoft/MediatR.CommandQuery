using System;
using System.Collections.Generic;
using Cosmos.Abstracts;
using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Cosmos.Tests.Data.Entities
{
    public class User : CosmosEntity, IHaveIdentifier<string>, ITrackCreated, ITrackUpdated
    {
        public string EmailAddress { get; set; }

        public bool IsEmailAddressConfirmed { get; set; }

        public string DisplayName { get; set; }

        public string PasswordHash { get; set; }

        public string ResetHash { get; set; }

        public string InviteHash { get; set; }

        public int AccessFailedCount { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public DateTimeOffset? LastLogin { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Created { get; set; }

        public string UpdatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }
    }
}
