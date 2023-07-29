using System;

using Cosmos.Abstracts;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Cosmos.Tests.Data.Entities;

public partial class Role : CosmosEntity, IHaveIdentifier<string>, ITrackCreated, ITrackUpdated
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset Created { get; set; }

    public string UpdatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }
}
