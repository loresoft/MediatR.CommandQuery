using MediatR.CommandQuery.Definitions;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

public class User : MongoEntity, IHaveIdentifier<string>, ITrackCreated, ITrackUpdated
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

    public string UpdatedBy { get; set; }
}
