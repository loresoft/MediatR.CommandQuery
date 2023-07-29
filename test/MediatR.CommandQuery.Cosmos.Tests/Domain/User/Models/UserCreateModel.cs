using System;

namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

public partial class UserCreateModel
    : EntityCreateModel
{
    #region Generated Properties
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

    #endregion

}
