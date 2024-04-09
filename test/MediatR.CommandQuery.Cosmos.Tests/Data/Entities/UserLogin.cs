using Cosmos.Abstracts;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Cosmos.Tests.Data.Entities;

public class UserLogin : CosmosEntity, IHaveIdentifier<string>, ITrackCreated, ITrackUpdated
{
    public string EmailAddress { get; set; }

    public Guid? UserId { get; set; }

    public string UserAgent { get; set; }

    public string Browser { get; set; }

    public string OperatingSystem { get; set; }

    public string DeviceFamily { get; set; }

    public string DeviceBrand { get; set; }

    public string DeviceModel { get; set; }

    public string IpAddress { get; set; }

    public bool IsSuccessful { get; set; }

    public string FailureMessage { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset Created { get; set; }

    public string UpdatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }
}
