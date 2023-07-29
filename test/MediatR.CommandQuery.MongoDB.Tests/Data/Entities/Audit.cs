using System;

using MediatR.CommandQuery.Definitions;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

public class Audit : MongoEntity, IHaveIdentifier<string>, ITrackCreated, ITrackUpdated
{
    public DateTime Date { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TaskId { get; set; }

    public string Content { get; set; }

    public string Username { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }
}
