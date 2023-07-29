using System;

namespace Tracker.WebService.Domain.Models;

public class UserLoginReadModel
    : EntityReadModel
{
    public string EmailAddress { get; set; } = null!;

    public Guid? UserId { get; set; }

    public string? UserAgent { get; set; }

    public string? Browser { get; set; }

    public string? OperatingSystem { get; set; }

    public string? DeviceFamily { get; set; }

    public string? DeviceBrand { get; set; }

    public string? DeviceModel { get; set; }

    public string? IpAddress { get; set; }

    public bool IsSuccessful { get; set; }

    public string? FailureMessage { get; set; }

}
