using Cosmos.Abstracts;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Cosmos.Tests.Data.Entities;

public class Audit : CosmosEntity, IHaveIdentifier<string>, ITrackCreated, ITrackUpdated
{
    public DateTime Date { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TaskId { get; set; }

    public string Content { get; set; }

    public string Username { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset Created { get; set; }

    public string UpdatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }
}
